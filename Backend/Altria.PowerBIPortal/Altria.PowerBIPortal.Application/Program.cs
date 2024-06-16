using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Application.Middleware;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Infrastructure.WindowsActiveDirectory;
using Altria.PowerBIPortal.Persistence;
using Altria.PowerBIPortal.Persistence.Repositories.ApprovalConfigs;
using Altria.PowerBIPortal.Persistence.Repositories.Subscriptions;
using Altria.PowerBIPortal.Persistence.Repositories.SubscriptionWhiteList;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEndpoints();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

#region Register dataContext / identity

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultDb"));
})
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DataContext>());

#endregion

#region Register authentication services

builder.Services.Configure<LdapOptions>(builder.Configuration.GetSection(LdapOptions.Options));
builder.Services.AddScoped<IExternalUserAuthenticator, ActiveDirectoryAuthenticator>();

builder.Services.AddAuthentication().AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
});
builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("AuthenticatedUser", policy =>  policy.RequireAuthenticatedUser());
});

builder.Services.AddScoped<RequestContext>();

#endregion

#region Register repositories

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionWhiteListEntryRepository, SubscriptionWhiteListEntryRepository>();
builder.Services.AddScoped<IApprovalOfficerRepository, ApprovalOfficerRepository>();

#endregion

var app = builder.Build();

app.UseCors("AllowAll");

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