using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class UserEntity : IdentityUser, IEntity
{
    public DateTime Created { get; private set; } = DateTime.UtcNow;
    public CollectionEntity Collection { get; private set; } = new();
    public ICollection<BattleEntity> BattlesInBot { get; private set; } = [];
    public ICollection<BattleEntity> BattlesInTop { get; private set; } = [];
}
