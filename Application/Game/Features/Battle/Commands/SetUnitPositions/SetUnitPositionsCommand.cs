using Application.Interfaces;
using Domain.Game.Models.Battle;
using MediatR;

namespace Application.Game.Features.Battle.Commands.SetUnitPositions;

public record SetUnitPositionsCommand : IRequest<SetUnitPositionsCommandResponse>, IBattleCommand
{
    public required string BattleId { get; init; }
    public required string UserId { get; init; }
    public ICollection<string> FrontUnitEntityIds { get; init; } = [];
    public ICollection<string> BackUnitEntityIds { get; init; } = [];
}
