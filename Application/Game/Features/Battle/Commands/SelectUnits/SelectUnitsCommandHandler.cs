using Application.Game.Features.Battle.Helpers.Abstraction;
using Application.Game.Features.Battle.Models;
using Application.Game.Models;
using Application.Game.Sessions;
using Application.Interfaces;
using Domain.Game.Models.Battle;
using MediatR;

namespace Application.Game.Features.Battle.Commands.SelectUnits;

public sealed class SelectUnitsCommandHandler(
    IContextStorage<BattleContextModel> contextStorage,
    IBattleUnitHelper battleUnitHelper,
    IBattleValidateHelper battleValidateHelper,
    IUnitEntityService unitEntityService)
    : IRequestHandler<SelectUnitsCommand, SelectUnitsCommandResponse>
{
    public Task<SelectUnitsCommandResponse> Handle(SelectUnitsCommand request, CancellationToken cancellationToken)
    {
        var filter = CreateFilter(request);

        var units = unitEntityService.GetRangeAsync(filter, cancellationToken);

        if (units.Count != request.UnitIds.Count) throw new ArgumentException();

        foreach (var unit in units)
        {
            var battleUnit = new BattleUnitModel
            {
                EntityId = unit.Id,
                UnitId = unit.UnitId,
                UserId = request.UserId
            };
            battleUnitHelper.AddUnit(battleUnit);
        }

        battleValidateHelper.EnsureDuplicateUnitsError(request.UserId);

        var battlefield = contextStorage.GetRequired();

        if (battlefield.TopUser.SelectedUnits.Count == 7 && 
            battlefield.BotUser.SelectedUnits.Count == 7)
        {
            battlefield.State = BattleStateEnum.UnitPlacement;
            battlefield.UserIdSelectingUnits = null;
        }
        else
        {
            battlefield.UserIdSelectingUnits = battlefield.UserIdSelectingUnits == battlefield.TopUser.UserId
                ? battlefield.BotUser.UserId
                : battlefield.TopUser.UserId;
        }

        var response = new SelectUnitsCommandResponse
        {
            UserIdSelectingUnits = battlefield.UserIdSelectingUnits
        };

        return Task.FromResult(response);
    }

    private UnitEntityFilter CreateFilter(SelectUnitsCommand request)
    {
        return new()
        {
            Ids = request.UnitIds,
            UserId = request.UserId
        };
    }
}
