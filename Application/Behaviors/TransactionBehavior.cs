using Application.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse>(
    ITransactionManager transactionManager,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ITransactionBehaviorSupport
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            if (transactionManager.HasTransaction)
            {
                return await next(cancellationToken);
            }

            await transactionManager.BeginTransactionAsync(cancellationToken);

            logger.LogInformation("Create transaction.");

            TResponse response = await next(cancellationToken);

            await transactionManager.CommitAsync(cancellationToken);

            logger.LogInformation("Transaction commited.");

            return response;
        }
        catch (Exception ex)
        {
            if (transactionManager.HasTransaction)
            {
                await transactionManager.RollbackAsync(cancellationToken);
                logger.LogError(ex, "Transaction rollback.");
            }

            throw;
        }
    }
}
