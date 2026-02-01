using Domain.Common;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class UnitEntity : IEntity
{
    public string Id { get; private set; }
    public string UnitId { get; private set; }
    public int Level { get; private set; }

    public List<AbilityValueObject> Abilities { get; private set; } = [];

    public string UserId { get; private set; } = null!;
    public UserEntity User { get; private set; } = null!;

    protected UnitEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public UnitEntity(string unitId) : this()
    {
        UnitId = unitId;
    }

    public UnitEntity(UserEntity user, string unitId) : this(unitId)
    {
        User = user;
    }

    public UnitEntity(string userId, string unitId) : this(unitId)
    {
        UserId = userId;
    }

    public void UpLevel()
    {
        Level += 1;
    }

    public void UpAbilityLevel(string abilityId)
    {
        var ability = Abilities.FirstOrDefault(x => x.Id == abilityId)
            ?? throw new CoreRequestException()
                .AddMessages(["Способность не найдена"]);
        ability.Level += 1;
    }
}
