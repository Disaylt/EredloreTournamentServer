namespace Application.Services.Abstraction;

public interface IBattleLockService
{
    SemaphoreSlim GetOrCreate(string battleId);
    bool Delete(string battleId);
}
