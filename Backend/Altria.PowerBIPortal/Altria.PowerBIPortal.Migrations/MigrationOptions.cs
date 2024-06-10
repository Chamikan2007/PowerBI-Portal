namespace Altria.PowerBIPortal.Migrations;

internal class MigrationOptions
{
    public bool EnsureDatabase { get; set; }

    public required string ConnectionString { get; set; }
}
