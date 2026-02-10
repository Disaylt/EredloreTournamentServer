namespace Domain.Game.Models.Units;

public interface IUnitDefaultCharacteristic
{
    string Id { get; }
    double MaxHealth { get;}
    double MaxHealthLevelUp { get; }
    double? MaxMana { get; }
    double? MaxManaLevelUp { get; }
    double? ManaRegeneration { get; }
    double? ManaRegenerationLevelUp { get; }
    double Initiative { get; }
    double InitiativeLevelUp { get; }
    double? Speed { get; }
    double? SpeedLevelUp { get; }
    HabitatEnum Habitat { get; }
    ProtectionType ProtectionType { get; }
    AttackTypeEnum AttackType { get; }
    int PhysicalDefense { get; }
    int MagicalResistance { get; }
    int PoisonResistance { get; }
    RarityEnum Rarity { get; }
    UnitCreateSource UnitCreateSource { get; }
}
