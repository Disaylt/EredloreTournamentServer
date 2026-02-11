using Infrastructure.Models;

namespace WebApi.Common;

public static class ConfigurationsExtensions
{
    public static ConnectionStringOptions GetDatabaseConfig(this ConfigurationManager configurationManager)
    {
        return configurationManager.GetSection(Constants.ConnectionStringsSection).Get<ConnectionStringOptions>()
                ?? throw new NullReferenceException(nameof(ConnectionStringOptions));
    }

    public static AuthOptions GetAuthConfig(this ConfigurationManager configurationManager)
    {
        return configurationManager.GetSection(Constants.AuthSection).Get<AuthOptions>()
                ?? throw new NullReferenceException(nameof(ConnectionStringOptions));
    }
}
