using Domain.Common.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class BattleEntity : IEntity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public bool IsColsed { get; set; }
    public BattleTypeEnum Type { get; init; }
    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public string TopUserId { get; private set; } = null!;
    public UserEntity TopUser { get; private set; } = null!;

    public string BotUserId { get; private set; } = null!;
    public UserEntity BotUser { get; private set; } = null!;

    protected BattleEntity() { }

    public BattleEntity(string topUserId, string botUserId)
    {
        TopUserId = topUserId;
        BotUserId = botUserId;
    }
}
