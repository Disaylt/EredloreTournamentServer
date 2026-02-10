namespace Application.Game.Features.Battle.Models;

public class UserInfo
{
    public bool CompleteUnitPlacment { get; set; } = false;
    public required string UserId { get; init; }
    public IReadOnlyList<PositionInfo>[] PositionsRows { get; init; } = new IReadOnlyList<PositionInfo>[2];
    public ICollection<BattleUnitModel> SelectedUnits { get; init; } = [];
}
