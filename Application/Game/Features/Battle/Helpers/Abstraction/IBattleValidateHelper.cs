namespace Application.Game.Features.Battle.Helpers.Abstraction;

public interface IBattleValidateHelper
{
    void EnsureDuplicateUnitsError(string userId);
    bool ValidateDuplicateUnitsError(string userId);
}
