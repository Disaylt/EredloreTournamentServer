using Application.Features.Auth.Commands.Create;
using Application.Features.Auth.Models;
using Application.Features.Auth.Services.Abstraction;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Auth.Commands.Refresh;

public sealed class RefreshAuthInfoCommandHandler(
    IMediator mediator,
    ISessionRepository sessionRepository,
    ITokenService<RefreshAuthInfo> refreshTokenService)
    : IRequestHandler<RefreshAuthInfoCommand, AuthInfoDto>
{
    public async Task<AuthInfoDto> Handle(RefreshAuthInfoCommand request, CancellationToken cancellationToken)
    {
        RefreshAuthInfo refreshAuthInfo = refreshTokenService.Read(request.RefreshToken);

        var session = await sessionRepository.FindByIdRequiredAsync(refreshAuthInfo.SessionId);

        EnsureActiveSession(session);
        EnsureValidSessionToken(session.Token, request.RefreshToken);

        var createAuthInfoCommand = new CreateAuthInfoCommand
        {
            SessionId = session.Id,
            UserId = session.UserId
        };

        var authInfo = await mediator.Send(createAuthInfoCommand, cancellationToken);

        session.SetNewRefreshToken(authInfo.RefreshToken);

        return authInfo;
    }

    private void EnsureValidSessionToken(string? sessionToken, string requestToken)
    {
        if (sessionToken is null || sessionToken != requestToken)
        {
            throw new CoreRequestException()
                .AddMessages(["Токен не подходит для продолжения сессии"])
                .SetStatusCode(System.Net.HttpStatusCode.BadRequest);
        }
    }

    private void EnsureActiveSession(SessionEntity session)
    {
        if (session.IsActive is false)
        {
            throw new CoreRequestException()
                .AddMessages(["Сессия не активна"])
                .SetStatusCode(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
