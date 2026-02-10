using Domain.Interfaces;

namespace Domain.Entities;

public class CollectionEntity : IEntity
{
    public string UserId { get; private set; } = null!;
    public UserEntity User { get; private set; } = null!;

    public int CommonDust { get; set; }
    public int RareDust { get; set; }
    public int EpicDust { get; set; }
    public int LegendaryDust { get; set; }
    public List<UnitEntity> Units { get; private set; } = [];
}
