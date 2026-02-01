namespace Domain.Game.Units;

public abstract class UnitBase
{
    public abstract string Id { get; }
    public abstract double Health { get; }
    public abstract double Mana { get; }
    public abstract double Initiative { get; }
    public abstract int Level { get; }
}
