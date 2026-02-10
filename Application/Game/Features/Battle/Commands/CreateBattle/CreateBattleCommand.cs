using Application.Models;
using Domain.Common.Enums;

namespace Application.Game.Features.Battle.Commands.CreateBattle;

public record CreateBattleCommand : CommandBase<string>
{
    public IReadOnlyCollection<string> UserIds { get; init; } = [];
    public required BattleTypeEnum Type { get; init; }
}
