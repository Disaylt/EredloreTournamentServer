using Application.Features.Auth.Commands.Create;
using Application.Features.Auth.Models;
using Application.Features.Users.Services.Abstraction;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.Login;

public sealed class LoginUserCommandHandler(
    IMediator mediator,
    IRepositoryBase<SessionEntity> sessionRepositoryBase,
    IUserService userService)
    : IRequestHandler<LoginUserCommand, AuthInfoDto>
{
    public async Task<AuthInfoDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (await userService.VerifyPasswordAsync(request.Email, request.Password) is false)
        {
            throw new CoreRequestException().AddMessages(["Логин или пароль введен неверно."]);
        }

        var user = await userService.FindByEmailAsync(request.Email);

        var session = new SessionEntity(user);

        var createAuthInfoCommand = new CreateAuthInfoCommand
        {
            SessionId = session.Id,
            UserId = user.Id
        };

        var authInfo = await mediator.Send(createAuthInfoCommand, cancellationToken);

        session.SetNewRefreshToken(authInfo.RefreshToken);
        sessionRepositoryBase.Add(session);

        return authInfo;
    }
}
