using Application.Game.Features.Battle.Models;
using Application.Game.Features.Battle.Validators;
using Application.Interfaces;
using FluentValidation;

namespace Application.Game.Features.Battle.Commands.SelectUnits;

public class SelectUnitsCommandValidator : AbstractValidator<SelectUnitsCommand>
{
    public SelectUnitsCommandValidator(
        IContextStorage<BattleContextModel> contextStorage,
        IBattleValidatorBase battleValidatorBase)
    {
        Include(battleValidatorBase);
        RuleFor(x => x.UserId).Must(value => contextStorage.Get()?.UserIdSelectingUnits == value);
        RuleFor(x => x.UnitIds)
            .Must(units =>
            {
                var battle = contextStorage.GetRequired();
                var totalUnits = battle.TopUser.SelectedUnits.Count + battle.BotUser.SelectedUnits.Count;

                if (totalUnits >= 14) return false;

                if (totalUnits == 0 || totalUnits == 13)
                {
                    return units.Count == 1;
                }

                return units.Count == 2;
            });
    }
}
