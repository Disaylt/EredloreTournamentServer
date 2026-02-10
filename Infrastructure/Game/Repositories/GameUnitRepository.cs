using Domain.Game.Models.Units;
using Domain.Game.Repositories;
using Microsoft.Extensions.Options;

namespace Infrastructure.Game.Repositories;

public class GameUnitRepository : IGameUnitRepository
{
    private readonly IReadOnlyCollection<UnitDefaultCharacteristic> _allUnits;
    private readonly Dictionary<UnitCreateSource, List<UnitDefaultCharacteristic>> _unitsBySource;
    private readonly Dictionary<string, UnitDefaultCharacteristic> _unitsById;

    public GameUnitRepository(IOptions<IReadOnlyCollection<UnitDefaultCharacteristic>> unitsConfig)
    {
        _allUnits = unitsConfig.Value;

        _unitsBySource = unitsConfig
            .Value
            .GroupBy(x => x.UnitCreateSource)
            .ToDictionary(x => x.Key, x => x.ToList());

        _unitsById = unitsConfig.Value.ToDictionary(x => x.Id);
    }

    public IUnitDefaultCharacteristic? Find(string id) => _unitsById.GetValueOrDefault(id);

    public IUnitDefaultCharacteristic FindRequired(string id) => 
        _unitsById.GetValueOrDefault(id) 
        ?? throw new NullReferenceException();

    public IReadOnlyCollection<IUnitDefaultCharacteristic> GetRange() => _allUnits;

    public IReadOnlyCollection<IUnitDefaultCharacteristic> GetRangeBySource(UnitCreateSource unitCreateSource) =>
        _unitsBySource.GetValueOrDefault(unitCreateSource) ?? [];
}
