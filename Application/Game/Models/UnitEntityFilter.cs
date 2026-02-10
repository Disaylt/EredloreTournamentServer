namespace Application.Game.Models;

public record UnitEntityFilter
{
    public ICollection<string> Ids { get; init; } = [];
    public string? UserId { get; init; }
}
