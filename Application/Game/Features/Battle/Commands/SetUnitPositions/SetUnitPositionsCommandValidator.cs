using Application.Game.Features.Battle.Models;
using Application.Interfaces;
using FluentValidation;

namespace Application.Game.Features.Battle.Commands.SetUnitPositions;

public class SetUnitPositionsCommandValidator : AbstractValidator<SetUnitPositionsCommand>
{
    public SetUnitPositionsCommandValidator(IContextStorage<BattleContextModel> contextStorage)
    {
        RuleFor(x => x.FrontUnitEntityIds.Count).LessThanOrEqualTo(4);
        RuleFor(x => x.BackUnitEntityIds.Count).LessThanOrEqualTo(4);
        RuleFor(x => x).Must(x => x.BackUnitEntityIds.Count + x.FrontUnitEntityIds.Count <= 7);
        RuleFor(x => x).Must(x =>
        {
            var selectedUnitEntityIds = contextStorage
                .GetRequired()
                .GetUserRequired(x.UserId)
                .SelectedUnits
                .Select(x => x.EntityId)
                .ToHashSet();

            return x.FrontUnitEntityIds
                .Concat(x.BackUnitEntityIds)
                .All(e => selectedUnitEntityIds.Contains(e));
        });
    }
}
