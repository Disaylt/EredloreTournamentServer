using Application.Helpers;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviors;

public class FluentValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResults = await HandleValidatorsAsync(request, cancellationToken);
        var errorDetails = ValidationHelper.CollectErrorDetails(validationResults);

        if (errorDetails.Length > 0)
        {
            throw ValidationHelper.CreateException(errorDetails);
        }

        return await next(cancellationToken);
    }

    private async Task<ValidationResult[]> HandleValidatorsAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (validators.Any() == false)
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        return await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));
    }
}
