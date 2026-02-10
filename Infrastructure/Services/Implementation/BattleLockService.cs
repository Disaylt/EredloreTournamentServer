using System.Collections.Concurrent;
using Application.Services.Abstraction;

namespace Infrastructure.Services.Implementation;

public class BattleLockService : IBattleLockService
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphors = new(Environment.ProcessorCount * 2, 1000);

    public bool Delete(string battleId)
    {
        return _semaphors.TryRemove(battleId, out _);
    }

    public SemaphoreSlim GetOrCreate(string battleId)
    {
        return _semaphors.GetOrAdd(
            battleId,
            _ => new SemaphoreSlim(1, 1)
        );
    }
}
