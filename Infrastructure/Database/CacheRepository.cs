using System.Text.Json;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Database;

public class CacheRepository(IDistributedCache distributedCache) : ICacheRepository
{
    public async Task SetAsync<T>(string key, T entity, TimeSpan? expire, CancellationToken ct)
    {
        var entityKey = $"{nameof(T)}_{key}";
        var value = JsonSerializer.Serialize(entity);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expire
        };

        await distributedCache.SetStringAsync(entityKey, value, options, ct);
    }

    public async Task DeleteAsync<T>(string key, CancellationToken ct)
    {
        var entityKey = $"{nameof(T)}_{key}";

        await distributedCache.RemoveAsync(entityKey, ct);
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken ct)
    {
        var entityKey = $"{nameof(T)}_{key}";

        var vaule = await distributedCache.GetStringAsync(entityKey, ct)
            ?? throw new NullReferenceException($"Key: {entityKey} not found.");

        return JsonSerializer.Deserialize<T>(vaule)
            ?? throw new NullReferenceException();
    }
}
