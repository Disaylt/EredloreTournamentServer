using Application.Game.Features.Battle.Helpers.Abstraction;
using Application.Game.Features.Battle.Helpers.Implementation;
using Application.Game.Features.Battle.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Game.Features.Battle.Extensions;

internal static class DependenciesExtensions
{
    public static void AddBattleServices(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddScoped<IBattleUnitHelper, BattleUnitHelper>();
        serviceDescriptors.AddScoped<IBattleValidateHelper, BattleValidateHelper>();
        serviceDescriptors.AddSingleton<IMapHelper, MapHelper>();

        serviceDescriptors.AddTransient<IBattleValidatorBase, BattleValidatorBase>();
    }
}
