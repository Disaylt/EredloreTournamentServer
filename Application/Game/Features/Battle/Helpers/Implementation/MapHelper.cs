using Application.Game.Features.Battle.Helpers.Abstraction;
using Domain.Game.Models.Units;

namespace Application.Game.Features.Battle.Helpers.Implementation;

public class MapHelper : IMapHelper
{
    private static IList<HabitatEnum> _mapTypes =
    [
        HabitatEnum.Forests,
        HabitatEnum.Mountains,
        HabitatEnum.Water,
        HabitatEnum.Wasteland
    ];

    public HabitatEnum SelectRandomMapType()
    {
        var randomIndex = Random.Shared.Next(_mapTypes.Count);

        return _mapTypes[randomIndex];
    }
}
