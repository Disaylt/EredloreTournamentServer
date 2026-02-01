using System.Reflection;
using Application.Behaviors;
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
            cfg.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });

        return serviceDescriptors;
    }
}
