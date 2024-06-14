using Altria.PowerBIPortal.Application.Infrastructure;
using Altria.PowerBIPortal.Application.Middleware;
using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Infrastructure.WindowsActiveDirectory;
using Altria.PowerBIPortal.Persistence;
using Altria.PowerBIPortal.Persistence.Repositories.SubscriptionRequests;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEndpoints();

#region Register dataContext / identity

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultDb"));
})
    .AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<IUnitOfWork, DataContext>();

#endregion

#region Register repositories

builder.Services.AddScoped<ISubscriptionRequestRepository, SubscriptionRequestRepository>();

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

var app = builder.Build();

app.UseEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestContextResolverMiddleware>();

app.Run();