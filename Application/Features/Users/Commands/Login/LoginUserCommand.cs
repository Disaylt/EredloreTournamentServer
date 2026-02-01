using Application.Features.Auth.Models;
using Application.Models;

namespace Application.Features.Users.Commands.Login;

public sealed record LoginUserCommand : CommandBase<AuthInfoDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
