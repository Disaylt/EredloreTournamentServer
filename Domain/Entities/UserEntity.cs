using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class UserEntity : IdentityUser, IEntity
{
    public DateTime Created { get; private set; } = DateTime.UtcNow;
    public int CommonDust { get; set; }
    public int RareDust { get; set; }
    public int EpicDust { get; set; }
    public int LegendaryDust { get; set; }
    public List<UnitEntity> Units { get; private set; } = [];
}
