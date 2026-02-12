namespace Domain.Entities;

public class UserResourcesEntity
{
    public string UserId { get; private set; } = null!;
    public UserEntity User { get; private set; } = null!;

    public int NumGold { get; set; }
    public int NumSilver { get; set; }
}
