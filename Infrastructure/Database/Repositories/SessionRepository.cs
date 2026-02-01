using Domain.Common;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Database.Repositories;

public sealed class SessionRepository(AreploreTournamentDbContext dbContext)
    : RepositoryBase<SessionEntity>(dbContext), ISessionRepository
{
    public async Task<SessionEntity> FindByIdRequiredAsync(string id)
    {
        return await Repository.FindAsync(id)
            ?? throw new CoreRequestException()
                .SetStatusCode(System.Net.HttpStatusCode.NotFound)
                .AddMessages(["Сессия не найдена."]);
    }
}
