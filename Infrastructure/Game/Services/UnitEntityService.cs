using Application.Game.Models;
using Application.Game.Services;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Game.Services;

public sealed class UnitEntityService(AreploreTournamentDbContext dbContext)
    : IUnitEntityService
{
    public async Task<ICollection<UnitEntity>> GetRangeAsync(UnitEntityFilter filter, CancellationToken cancellationToken)
    {
        var query = dbContext.Units.AsNoTracking();

        if (filter.Ids is not null)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }

        if (filter.UserId is not null)
        {
            query = query.Where(x => x.UserCollectionId == filter.UserId);
        }

        return await query.ToListAsync(cancellationToken);
    }
}
