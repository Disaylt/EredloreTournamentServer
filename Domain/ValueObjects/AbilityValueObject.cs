namespace Domain.ValueObjects;

public sealed class AbilityValueObject(string id)
{
    public string Id { get; private set; } = id;
    public int Level { get; set; }
}
