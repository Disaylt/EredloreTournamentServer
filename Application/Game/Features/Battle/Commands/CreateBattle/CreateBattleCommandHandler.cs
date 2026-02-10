using Application.Game.Features.Battle.Models;
using Application.Game.Features.Battle.Helpers.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Game.Features.Battle.Commands.CreateBattle;

public class CreateBattleCommandHandler(
    IMapHelper mapHelper,
    IRepositoryBase<BattleEntity> battleRepository,
    ICacheRepository cacheRepository)
    : IRequestHandler<CreateBattleCommand, string>
{
    public async Task<string> Handle(CreateBattleCommand request, CancellationToken cancellationToken)
    {
        var battleEntity = new BattleEntity(
            request.UserIds.First(),
            request.UserIds.Last())
        {
            IsColsed = false,
            Type = request.Type
        };

        battleRepository.Add(battleEntity);

        var battleContext = new BattleContextModel
        {
            Id = battleEntity.Id,
            MapType = mapHelper.SelectRandomMapType(),
            TopUser = new()
            {
                UserId = battleEntity.TopUserId
            },
            BotUser = new()
            {
                UserId = battleEntity.BotUserId
            },
            UserIdSelectingUnits = battleEntity.BotUserId,
        };

        await cacheRepository.SetAsync(battleEntity.Id, battleContext, null, cancellationToken);

        return battleEntity.Id;
    }
}
