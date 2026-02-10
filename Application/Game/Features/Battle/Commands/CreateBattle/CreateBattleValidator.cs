using FluentValidation;

namespace Application.Game.Features.Battle.Commands.CreateBattle;

public class CreateBattleValidator : AbstractValidator<CreateBattleCommand>
{
    public CreateBattleValidator()
    {
        RuleFor(x => x.UserIds).Must(x => x.Distinct().Count() == 2);
        RuleFor(x => x.Type).IsInEnum();
    }
}
