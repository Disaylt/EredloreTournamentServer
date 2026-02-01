using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Repositories;

public interface ISessionRepository : IRepositoryBase<SessionEntity>
{
    Task<SessionEntity> FindByIdRequiredAsync(string id);
}
