using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Infrastructure.Database;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Common;

public static class DependenciesExtensions
{
    public static IServiceCollection AddAppVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static void AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(Constants.AuthSection));
        builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(Constants.ConnectionStringsSection));
    }

    public static IServiceCollection AddWebApiAuth(this IServiceCollection services, AuthOptions opt)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidIssuer = opt.ValidIssuer,
                    ValidAudience = opt.ValidAudience,
                    ValidateIssuer = opt.IsCheckValidIssuer,
                    ValidateAudience = opt.IsCheckValidAudience,
                    ValidateLifetime = opt.IsCheckExpireDate,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(opt.AuthSecret))
                };
            });

        return services;
    }

    public static async Task RunMigration(this WebApplication webApplication)
    {
        bool isSuccess = false;
        using var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AreploreTournamentDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        while (!isSuccess)
        {
            try
            {
                logger.LogInformation("Try migrate");

                await dbContext.Database.MigrateAsync();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Bad migration.");

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        logger.LogInformation("Migration apply");
    }
}
