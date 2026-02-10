using Application.Game.Features.Battle.Models;
using Application.Interfaces;
using Domain.Game.Models.Battle;
using MediatR;

namespace Application.Game.Features.Battle.Commands.SetUnitPositions;

internal class SetUnitPositionsCommandHandler(
    IContextStorage<BattleContextModel> contextStorage)
    : IRequestHandler<SetUnitPositionsCommand, SetUnitPositionsCommandResponse>
{
    public Task<SetUnitPositionsCommandResponse> Handle(SetUnitPositionsCommand request, CancellationToken cancellationToken)
    {
        var user = contextStorage.GetRequired().GetUserRequired(request.UserId);
        var entityIdAndUnitPairs = user.SelectedUnits.ToDictionary(x => x.EntityId);


        user.PositionsRows[0] = request
            .FrontUnitEntityIds
            .Select(x => new PositionInfo
            {
                Unit = entityIdAndUnitPairs[x]
            })
            .ToList();

        user.PositionsRows[0] = request
            .BackUnitEntityIds
            .Select(x => new PositionInfo
            {
                Unit = entityIdAndUnitPairs[x]
            })
            .ToList();

        List<string> idsToDelete = [];

        var battleField = contextStorage.GetRequired();

        user.CompleteUnitPlacment = true;

        if (battleField.TopUser.CompleteUnitPlacment && battleField.BotUser.CompleteUnitPlacment)
        {
            battleField.State = BattleStateEnum.Battle;
        }

        return Task.FromResult(new SetUnitPositionsCommandResponse { });
    }
}
