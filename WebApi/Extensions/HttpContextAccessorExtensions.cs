using System.Security.Claims;
using Domain.Common;
using Infrastructure.Features.Auth.Extensions;

namespace WebApi.Extensions;

public static class HttpContextAccessorExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FindByType(ClaimTypes.NameIdentifier)
            ?? throw new CoreRequestException()
                .SetStatusCode(System.Net.HttpStatusCode.Unauthorized)
                .AddMessages(["Идентификатор пользователя не найден."]);
    }
}
