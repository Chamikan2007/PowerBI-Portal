using DbUp;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Altria.PowerBIPortal.Migrations;

internal class MigrationManager
{
    private readonly MigrationOptions _options;
    private readonly ILogger<MigrationManager> _logger;

    public MigrationManager(MigrationOptions options, ILogger<MigrationManager> logger)
    {
        _options = options;
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation("Starting database migrations");

        ExecuteMigrations();

        _logger.LogInformation("Database migrations completed successfully");
    }

    private void ExecuteMigrations()
    {
        _logger.LogInformation($"Updating core database...");

        PerformDbMigration(_options.ConnectionString, _options.EnsureDatabase);

        _logger.LogInformation("Core database updated successfully.");
    }

    private static void PerformDbMigration(string connectionString, bool ensureDatabase)
    {
        var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), constr => Regex.IsMatch(constr, $".Scripts.*\\.sql$", RegexOptions.Singleline))
                .LogToAutodetectedLog()
                .JournalToSqlTable("dbo", "__SchemaVersions")
                .Build();

        if (ensureDatabase)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);
        }

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception("Error occured while performing database migration.", result.Error);
        }
    }
}
