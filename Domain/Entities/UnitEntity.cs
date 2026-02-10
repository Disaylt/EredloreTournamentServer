using Domain.Common;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class UnitEntity : IEntity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string UnitId { get; init; }
    public int Level { get; private set; }
    public bool CanSell { get; init; }

    public List<AbilityValueObject> Abilities { get; private set; } = [];

    public string UserId { get; init; } = null!;
    public CollectionEntity UserCollection { get; init; } = null!;

    protected UnitEntity() { }

    public UnitEntity(CollectionEntity userCollection) : this()
    {
        UserCollection = userCollection;
    }

    public UnitEntity(string userId) : this()
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
