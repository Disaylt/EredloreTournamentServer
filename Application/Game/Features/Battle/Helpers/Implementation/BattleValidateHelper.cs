using Application.Game.Features.Battle.Helpers.Abstraction;
using Application.Game.Features.Battle.Models;
using Application.Interfaces;
using Domain.Game.Models.Units;
using Domain.Game.Repositories;

namespace Application.Game.Features.Battle.Helpers.Implementation;

public class BattleValidateHelper(
    IContextStorage<BattleContextModel> contextStorage, 
    IGameUnitRepository unitRepository) 
    : IBattleValidateHelper
{
    public void EnsureDuplicateUnitsError(string userId)
    {
        if (ValidateDuplicateUnitsError(userId) is false)
        {
            throw new ArgumentException();
        }
    }

    public bool ValidateDuplicateUnitsError(string userId)
    {
        var units = contextStorage
            .GetRequired()
            .GetUserRequired(userId)
            .SelectedUnits;

        if (units.Count == 0) return true;

        return units
            .GroupBy(v => v.UnitId)
            .All(g =>
            {
                var rarity = unitRepository.FindRequired(g.Key).Rarity;
                var count = g.Count();

                return rarity == RarityEnum.Legendary && count == 1 || 
                     rarity != RarityEnum.Legendary && count <= 2;
            });
    }
}
