using Application.Features.Auth.Interfaces;

namespace Application.Features.Auth.Models;

public class AccessAuthInfo : IAuthInfo
{
    public required string Id { get; init; }
    public required string UserId { get; init; }
    public required string SessionId { get; init; }
}
