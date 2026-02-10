using Application.Interfaces;
using MediatR;

namespace Application.Game.Features.Battle.Commands.SelectUnits;

public class SelectUnitsCommand : IRequest<SelectUnitsCommandResponse>, IBattleCommand
{
    public required string BattleId { get; init; }
    public required string UserId { get; init; }
    public ICollection<string> UnitIds { get; init; } = [];
}
