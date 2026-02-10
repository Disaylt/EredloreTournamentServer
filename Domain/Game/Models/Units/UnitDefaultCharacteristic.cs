namespace Domain.Game.Models.Units;

public sealed record UnitDefaultCharacteristic : IUnitDefaultCharacteristic
{
    public required string Id { get; init; }

    public double MaxHealth { get; init; }

    public double MaxHealthLevelUp { get; init; }

    public double? MaxMana { get; init; }

    public double? MaxManaLevelUp { get; init; }

    public double? ManaRegeneration { get; init; }

    public double? ManaRegenerationLevelUp { get; init; }

    public double Initiative { get; init; }

    public double InitiativeLevelUp { get; init; }

    public double? Speed { get; init; }

    public double? SpeedLevelUp { get; init; }

    public HabitatEnum Habitat { get; init; }

    public ProtectionType ProtectionType { get; init; }

    public AttackTypeEnum AttackType { get; init; }

    public int PhysicalDefense { get; init; }

    public int MagicalResistance { get; init; }

    public int PoisonResistance { get; init; }

    public RarityEnum Rarity { get; init; }

    public UnitCreateSource UnitCreateSource { get; init; }
}
