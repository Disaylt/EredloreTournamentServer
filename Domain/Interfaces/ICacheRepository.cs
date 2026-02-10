namespace Domain.Interfaces;

public interface ICacheRepository
{
    public Task SetAsync<T>(string key, T entity, TimeSpan? expire, CancellationToken ct);
    public Task DeleteAsync<T>(string key, CancellationToken ct);
    public Task<T> GetAsync<T>(string key, CancellationToken ct);
}
