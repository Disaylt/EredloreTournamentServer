namespace Domain.Interfaces;

public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    public TEntity Add(TEntity entity);
    public void AddRange(IEnumerable<TEntity> entities);
    public TEntity Delete(TEntity entity);
    public void DeleteRange(IEnumerable<TEntity> entities);
    public TEntity Update(TEntity entity);
    public void UpdateRange(IEnumerable<TEntity> entities);
}
