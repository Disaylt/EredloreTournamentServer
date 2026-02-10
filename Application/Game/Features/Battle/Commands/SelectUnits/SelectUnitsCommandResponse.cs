namespace Application.Game.Features.Battle.Commands.SelectUnits;

public record SelectUnitsCommandResponse
{
    public string? UserIdSelectingUnits { get; init; }
}
