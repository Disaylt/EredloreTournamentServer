using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.VisualBasic;

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
