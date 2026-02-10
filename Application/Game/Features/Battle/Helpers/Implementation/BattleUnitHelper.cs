using Application.Game.Features.Battle.Helpers.Abstraction;
using Application.Game.Features.Battle.Models;
using Application.Interfaces;

namespace Application.Game.Features.Battle.Helpers.Implementation;

public class BattleUnitHelper(IContextStorage<BattleContextModel> contextStorage) : IBattleUnitHelper
{
    public void AddUnit(BattleUnitModel unit)
    {

    }
}
