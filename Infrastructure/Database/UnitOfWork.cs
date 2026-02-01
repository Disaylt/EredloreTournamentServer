using Domain.Interfaces;

namespace Infrastructure.Database;

public class UnitOfWork(AreploreTournamentDbContext dbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
