using Application.Interfaces;
using Domain.Game.Models.Battle;
using Domain.Game.Models.Units;

namespace Application.Game.Features.Battle.Models;

public class BattleContextModel : IContext
{
    public required string Id { get; init; }
    public required HabitatEnum MapType { get; init; }
    public required UserInfo TopUser { get; init; }
    public required UserInfo BotUser { get; init; }
    public string? UserIdSelectingUnits { get; set; }
    public BattleStateEnum State { get; set; } = BattleStateEnum.WaitPlayers;
    public DateTime UpdateDateTime { get; private set; } = DateTime.UtcNow;
    public int MoveCounter { get; private set; } = 0;

    public void IncrementMoveCounter() => MoveCounter += 1;

    public UserInfo GetUserRequired(string userId)
    {
        if (TopUser.UserId == userId) return TopUser;
        if (BotUser.UserId == userId) return BotUser;

        throw new ArgumentException();
    }
}
