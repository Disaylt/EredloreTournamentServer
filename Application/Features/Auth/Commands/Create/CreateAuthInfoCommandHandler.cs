using Application.Features.Auth.Models;
using Application.Features.Auth.Services.Abstraction;
using MediatR;

namespace Application.Features.Auth.Commands.Create;

public sealed class CreateAuthInfoCommandHandler(
    ITokenService<AccessAuthInfo> accessTokenService,
    ITokenService<RefreshAuthInfo> refreshTokenService)
    : IRequestHandler<CreateAuthInfoCommand, AuthInfoDto>
{
    public Task<AuthInfoDto> Handle(CreateAuthInfoCommand request, CancellationToken cancellationToken)
    {
        var refreshAuthInfo = new RefreshAuthInfo { SessionId = request.SessionId };
        var refreshToken = refreshTokenService.Create(refreshAuthInfo);

        var accessAuthInfo = new AccessAuthInfo
        {
            UserId = request.UserId,
            SessionId = request.SessionId,
            Id = Guid.NewGuid().ToString()
        };
        var accessToken = accessTokenService.Create(accessAuthInfo);

        var response = new AuthInfoDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return Task.FromResult(response);
    }
}
