﻿using Altria.PowerBIPortal.Persistence;
using Altria.PowerBIPortal.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Altria.PowerBIPortal.Migrations;

internal class DataContextContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var config = Configurations.GetConfigurations();

        var connectionString = config.GetConnectionString("defaultDb")!;

        var builder = new DbContextOptionsBuilder<DataContext>();
        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DataContextContextFactory).Assembly.FullName));

        var context = new DataContext(builder.Options);

        return context;
    }
}
