namespace Application.Game.Features.Battle.Models;

public class PositionInfo
{
    public required BattleUnitModel Unit { get; init; }
    public BattleUnitModel?[] Minions { get; init; } = new BattleUnitModel?[2];
}
