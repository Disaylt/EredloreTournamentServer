using Application.Features.Auth.Models;
using Application.Interfaces;
using Application.Models;

namespace Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand : CommandBase<AuthInfoDto>, ITransactionBehaviorSupport
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
