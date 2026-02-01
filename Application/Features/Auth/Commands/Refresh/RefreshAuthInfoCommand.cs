using Application.Features.Auth.Models;
using Application.Models;

namespace Application.Features.Auth.Commands.Refresh;

public sealed record RefreshAuthInfoCommand : CommandBase<AuthInfoDto>
{
    public required string RefreshToken { get; init; }
}
