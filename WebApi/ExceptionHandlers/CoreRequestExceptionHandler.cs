using Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.ExceptionHandlers;

public class CoreRequestExceptionHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CoreRequestException coreRequestException)
        {
            return false;
        }

        var problemDetailsContext = new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
                {
                    Status = (int)coreRequestException.StatusCode,
                    Title = "Ошибка обработки данных.",
                    Detail = "Приложение не смогло обработать ваш запрос. Пожалуйста исправьте ошибки и попробуйте снова. Либо обратитесь в службу поддержки."
                }
        };

        problemDetailsContext.ProblemDetails.Extensions.TryAdd("errors", coreRequestException.KeyAndMessagesPairs);

        return await problemDetailsService.TryWriteAsync(problemDetailsContext);
    }
}
