using Application.Features.Auth.Interfaces;

namespace Application.Features.Auth.Models;

public class RefreshAuthInfo : IAuthInfo
{
    public required string SessionId { get; init; }
}
