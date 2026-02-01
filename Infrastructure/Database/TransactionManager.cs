using Domain.Interfaces;

namespace Infrastructure.Database;

public sealed class TransactionManager(AreploreTournamentDbContext dbContext) : ITransactionManager
{
    public bool HasTransaction => dbContext.Database.CurrentTransaction != null;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }
}
