using Domain.Interfaces;

namespace Domain.Entities;

public class CollectionEntity : IEntity
{
    public string UserId { get; private set; } = null!;
    public UserEntity User { get; private set; } = null!;

    public int CommonSoul { get; set; }
    public int RareSoul { get; set; }
    public int EpicSoul { get; set; }
    public int LegendarySoul { get; set; }
    public List<UnitEntity> Units { get; private set; } = [];
}
