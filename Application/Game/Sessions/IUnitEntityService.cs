using Application.Game.Models;
using Domain.Entities;

namespace Application.Game.Sessions;

public interface IUnitEntityService
{
    public ICollection<UnitEntity> GetRangeAsync(UnitEntityFilter filter, CancellationToken cancellationToken);
}
