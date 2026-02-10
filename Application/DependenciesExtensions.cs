using System.Reflection;
using Application.Behaviors;
using Application.Game.Features.Battle.Extensions;
using Application.Game.Features.Battle.Validators;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependenciesExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            cfg.AddOpenBehavior(typeof(BattleCommandBehavior<,>));
            cfg.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
        });

        serviceDescriptors.AddScoped(typeof(IContextStorage<>), typeof(ContextStorage<>));

        serviceDescriptors.AddBattleServices();

        return serviceDescriptors;
    }
}
