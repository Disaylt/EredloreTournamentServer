using Application.Game.Features.Battle.Models;
using Application.Interfaces;
using Application.Services.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Behaviors;

public class BattleCommandBehavior<TRequest, TResponse>(
    IContextStorage<BattleContextModel> contextStorage,
    IBattleLockService battleLockService,
    ICacheRepository cacheRepository)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBattleCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var battleLock = battleLockService.GetOrCreate(request.BattleId);

        await battleLock.WaitAsync(cancellationToken);

        try
        {
            var battle = await cacheRepository.GetAsync<BattleContextModel>(request.BattleId, cancellationToken);
            contextStorage.Set(battle);

            var result = await next(cancellationToken);

            await cacheRepository.SetAsync(request.BattleId, battle, null, cancellationToken);

            return result;
        }
        finally
        {
            battleLock.Release();
        }
    }
}
