using Domain.Entities;
using Domain.Events;
using Domain.Game.Models.Units;
using Domain.Game.Repositories;
using Domain.Interfaces;
using MediatR;

namespace Application.Game.Features.Units.EventHandlers;

public class AddStartKitEventHandler(
    IRepositoryBase<UnitEntity> unitRepositoryBase,
    IGameUnitRepository gameUnitRepository)
    : INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var startUnits = gameUnitRepository
            .GetRangeBySource(UnitCreateSource.StarterKit);

        foreach (var unit in startUnits)
        {
            for(int i = 0; i < 2; i++)
            {
                unitRepositoryBase.Add
                    (new UnitEntity(notification.User.Id)
                    {
                        CanSell = false,
                        UnitId = unit.Id,
                    });
            }
        }

        return Task.CompletedTask;
    }
}
