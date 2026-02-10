using Domain.Game.Models.Units;

namespace Domain.Game.Repositories;

public interface IGameUnitRepository
{
    IUnitDefaultCharacteristic? Find(string id);
    IUnitDefaultCharacteristic FindRequired(string id);
    IReadOnlyCollection<IUnitDefaultCharacteristic> GetRange();
    IReadOnlyCollection<IUnitDefaultCharacteristic> GetRangeBySource(UnitCreateSource unitCreateSource);
}
