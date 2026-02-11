using Application.Game.Models;
using Domain.Entities;

namespace Application.Game.Services;

public interface IUnitEntityService
{
    public Task<ICollection<UnitEntity>> GetRangeAsync(UnitEntityFilter filter, CancellationToken cancellationToken);
}
