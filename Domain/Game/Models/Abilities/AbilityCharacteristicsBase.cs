namespace Domain.Game.Models.Abilities;

public abstract class AbilityCharacteristicsBase
{
    public abstract string Id { get; }
    public abstract int Sort { get; }
    public int MaxLevel { get; }
}
