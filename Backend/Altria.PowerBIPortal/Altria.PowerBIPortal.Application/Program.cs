using Altria.PowerBIPortal.Application.BackgroundServices;
using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Application.Middleware;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Managers;
using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.DomainServices;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;
using Altria.PowerBIPortal.Infrastructure.PowerBIReports;
using Altria.PowerBIPortal.Infrastructure.WindowsActiveDirectory;
using Altria.PowerBIPortal.Persistence;
using Altria.PowerBIPortal.Persistence.Repositories.ApprovalConfigs;
using Altria.PowerBIPortal.Persistence.Repositories.SubscriberWhiteListEntries;
using Altria.PowerBIPortal.Persistence.Repositories.SubscriptionRequests;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEndpoints();

#region Add CORS policies

builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!;

    options.AddPolicy("AllowFrontends", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

#endregion

#region Register dataContext / identity

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultDb"));
})
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DataContext>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DataContext>());

#endregion

#region Register authentication services

builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieSettings = builder.Configuration.GetSection("CookieSettings");
    var sameSiteMode = SameSiteMode.Strict;
    if (cookieSettings != null)
    {
        sameSiteMode = cookieSettings.GetValue("SameSiteMode", sameSiteMode);
    }

    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = sameSiteMode;
    options.Cookie.Name = "Altria.PowerBIPortal.Auth";
    options.Cookie.Path = "/";
});

builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddScoped<RequestContext>();

builder.Services.Configure<LdapOptions>(builder.Configuration.GetSection(LdapOptions.Options));
builder.Services.AddScoped<IExternalUserAuthenticator, ActiveDirectoryAuthenticator>();

#endregion

#region Register repositories

builder.Services.AddScoped<ISubscriptionRequestRepository, SubscriptionRequestRepository>();
builder.Services.AddScoped<ISubscriberWhiteListEntryRepository, SubscriberWhiteListRepository>();
builder.Services.AddScoped<IApprovalOfficerRepository, ApprovalOfficerRepository>();

#endregion

#region Register domain services

builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

#endregion

#region Power BI report service

var reportConfig = builder.Configuration.GetRequiredSection("ReportServiceConfiguration");

builder.Services.AddHttpClient<IPowerBIReportService, PowerBIReportService>((services, client) =>
    {
        client.BaseAddress = reportConfig.GetValue<Uri>("BaseUrl");

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var userName = reportConfig.GetValue<string>("UserName");
        var password = reportConfig.GetValue<string>("Password");

        var handler = new SocketsHttpHandler()
        {
            Credentials = new NetworkCredential(userName, password),
            PooledConnectionLifetime = TimeSpan.FromMinutes(5),
        };
        return handler;
    })
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

#endregion

#region Register hosted services

builder.Services.AddHostedService<SubscripionsProcessor>();

#endregion

var app = builder.Build();

app.UseCors("AllowFrontends");

app.UseEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestContextResolverMiddleware>();

app.Run();