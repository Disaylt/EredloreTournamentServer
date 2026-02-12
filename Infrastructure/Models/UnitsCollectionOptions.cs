using Domain.Game.Models.Units;

namespace Infrastructure.Models;

public class UnitsCollectionOptions
{
    public IReadOnlyCollection<UnitDefaultCharacteristic>? Units { get; init; }

    public IReadOnlyCollection<UnitDefaultCharacteristic> GetRequired() =>
        Units ?? throw new NullReferenceException();
}
