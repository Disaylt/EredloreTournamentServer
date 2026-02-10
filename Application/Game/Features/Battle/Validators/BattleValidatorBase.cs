using Application.Game.Features.Battle.Models;
using Application.Interfaces;
using FluentValidation;

namespace Application.Game.Features.Battle.Validators;

public class BattleValidatorBase : AbstractValidator<IBattleCommand>, IBattleValidatorBase
{
    public BattleValidatorBase(IContextStorage<BattleContextModel> contextStorage)
    {
        RuleFor(x => x.BattleId).Equal(contextStorage.Get()?.Id);
    }
}
