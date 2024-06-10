using Altria.PowerBIPortal.Migrations;
using Altria.PowerBIPortal.Persistence;
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
            EnsureDatabase = false,
            ConnectionString = connectionString,
        };

#else

        var parseResult = Parser.Default.ParseArguments<MigrationOptions>(args);
        if (parseResult.Errors.Any())
        {
            var errors = parseResult.Errors.Select(e => e.Tag.ToString());
            throw new Exception(string.Join(Environment.NewLine, errors));
        }

        var options = parseResult.Value;

#endif
        services.AddTransient((s) => options);
    });
}