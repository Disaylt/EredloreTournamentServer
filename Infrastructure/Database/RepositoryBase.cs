using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class RepositoryBase<TEntity>(AreploreTournamentDbContext dbContext)
    : IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    protected readonly DbSet<TEntity> Repository = dbContext.Set<TEntity>();

    public TEntity Add(TEntity entity) => Repository.Add(entity).Entity;
    public void AddRange(IEnumerable<TEntity> entities) => Repository.AddRange(entities);
    public TEntity Delete(TEntity entity) => Repository.Remove(entity).Entity;
    public void DeleteRange(IEnumerable<TEntity> entities) => Repository.RemoveRange(entities);
    public TEntity Update(TEntity entity) => Repository.Update(entity).Entity;
    public void UpdateRange(IEnumerable<TEntity> entities) => Repository.UpdateRange(entities);
}
