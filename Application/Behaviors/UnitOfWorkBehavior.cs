using Application.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : CommandBase<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next(cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}
