using Altria.PowerBIPortal.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Altria.PowerBIPortal.Migrations;

internal class DesignTimeDataContextContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var config = Configurations.GetConfigurations();

        var connectionString = config.GetConnectionString("defaultDb")!;

        var builder = new DbContextOptionsBuilder<DataContext>();
        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DesignTimeDataContextContextFactory).Assembly.FullName));

        var context = new DataContext(builder.Options);

        return context;
    }
}
