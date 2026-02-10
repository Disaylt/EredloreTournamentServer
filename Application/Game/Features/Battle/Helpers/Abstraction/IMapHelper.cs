using Domain.Game.Models.Units;

namespace Application.Game.Features.Battle.Helpers.Abstraction;

public interface IMapHelper
{
    HabitatEnum SelectRandomMapType();
}
