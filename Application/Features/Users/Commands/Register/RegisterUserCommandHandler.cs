using Application.Features.Auth.Commands.Create;
using Application.Features.Auth.Models;
using Application.Features.Users.Services.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.Register;

public sealed class RegisterUserCommandHandler(
    IMediator mediator,
    IRepositoryBase<SessionEntity> sessionRepositoryBase,
    IUserService userService)
    : IRequestHandler<RegisterUserCommand, AuthInfoDto>
{
    public async Task<AuthInfoDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.CreateAsync(request.Email, request.Password);

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
