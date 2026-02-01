using Application.Features.Auth.Models;
using Application.Features.Auth.Services.Abstraction;
using Application.Features.Users.Services.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.Features.Auth.Services.Abstraction;
using Infrastructure.Features.Auth.Services.Implementation;
using Infrastructure.Features.Users.Services.Implementation;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<ISessionRepository, SessionRepository>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConnectionStringOptions databaseOptions)
    {
        services.AddNpgsql<AreploreTournamentDbContext>(databaseOptions.DatingPostgresqlDb);

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

        return services;
    }
}
