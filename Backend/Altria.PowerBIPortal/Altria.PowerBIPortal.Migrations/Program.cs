using Altria.PowerBIPortal.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();
var migrationManager = host.Services.GetRequiredService<MigrationManager>();

migrationManager.Execute();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddTransient<MigrationManager>();

#if DEBUG

        var config = Configurations.GetConfigurations();

        var connectionString = config.GetConnectionString("defaultDb")!;

        var options = new MigrationOptions
        {
            EnsureDatabase = true,
            ConnectionString = connectionString,
        };

#else

        var options = new MigrationOptions
        {
            ConnectionString = args[0],
        };

#endif
        services.AddTransient((s) => options);
    });
}