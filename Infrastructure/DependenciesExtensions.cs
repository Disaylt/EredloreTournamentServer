using Application.Features.Auth.Models;
using Application.Features.Auth.Services.Abstraction;
using Application.Features.Users.Services.Abstraction;
using Application.Game.Services;
using Application.Services.Abstraction;
using Domain.Entities;
using Domain.Game.Models.Units;
using Domain.Game.Repositories;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.Features.Auth.Services.Abstraction;
using Infrastructure.Features.Auth.Services.Implementation;
using Infrastructure.Features.Users.Services.Implementation;
using Infrastructure.Game.Repositories;
using Infrastructure.Game.Services;
using Infrastructure.Models;
using Infrastructure.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class DependenciesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<IJwtSecurityTokenHandler, CustomJwtSecurityTokenHandler>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        services.AddSingleton<IClaimsService<AccessAuthInfo>, JwtAccessClaimsService>();
        services.AddSingleton<ITokenService<AccessAuthInfo>, JwtAccessTokenService>();
        services.AddSingleton<IClaimsService<RefreshAuthInfo>, JwtRefreshClaimsService>();
        services.AddSingleton<ITokenService<RefreshAuthInfo>, JwtRefreshTokenService>();
        services.AddSingleton<IGameUnitRepository, GameUnitRepository>();

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddSingleton<IBattleLockService, BattleLockService>();

        services.AddScoped<IUnitEntityService, UnitEntityService>();

        return services;
    }

    public static void AddGameConfigs(this IHostApplicationBuilder builder)
    {
        var path = builder.Configuration.GetRequiredSection("ApplicationConfigsPath").Get<string>();

        builder.Configuration.AddJsonFile($"{path}Units.json",
            optional: false,
            reloadOnChange: false);

        builder.Services.Configure<UnitsCollectionOptions>(builder.Configuration);
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConnectionStringOptions databaseOptions)
    {
        services.AddNpgsql<AreploreTournamentDbContext>(databaseOptions.GamePostgresqlDb);

        services.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            options.User.RequireUniqueEmail = true;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddEntityFrameworkStores<AreploreTournamentDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddSingleton<ICacheRepository, CacheRepository>();

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = databaseOptions.Redis;
            opt.InstanceName = "Game";
        });

        return services;
    }
}
