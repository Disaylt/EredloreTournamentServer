using Domain.Interfaces;

namespace Domain.Entities;

public sealed class SessionEntity : IEntity
{
    public string Id { get; private set; }

    public DateTime LastUse { get; private set; } = DateTime.UtcNow;
    public DateTime Created { get; private set; } = DateTime.UtcNow;
    public bool IsActive { get; private set; } = true;
    public string? Token { get; private set; }

    public string UserId { get; private set; } = null!;
    public UserEntity User { get; private set; } = null!;

    protected SessionEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public SessionEntity(UserEntity user) : this()
    {
        User = user;
    }

    public SessionEntity(string userId) : this()
    {
        UserId = userId;
    }

    public SessionEntity Disable()
    {
        IsActive = false;
        UpdateLastUse();

        return this;
    }

    public SessionEntity SetNewRefreshToken(string token)
    {
        Token = token;
        UpdateLastUse();

        return this;
    }

    public SessionEntity UpdateLastUse()
    {
        LastUse = DateTime.UtcNow;

        return this;
    }
}
