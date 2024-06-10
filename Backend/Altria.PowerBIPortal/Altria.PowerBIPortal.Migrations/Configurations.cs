using Microsoft.Extensions.Configuration;

namespace Altria.PowerBIPortal.Migrations;

internal static class Configurations
{
    internal static IConfigurationRoot GetConfigurations()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddJsonFile("appsettings.json");
        return configBuilder.Build();
    }
}
