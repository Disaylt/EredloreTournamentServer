using Application.Features.Auth.Models;
using MediatR;

namespace Application.Features.Auth.Commands.Create;

public sealed record CreateAuthInfoCommand : IRequest<AuthInfoDto>
{
    public required string UserId { get; init; }
    public required string SessionId { get; init; }
}
