namespace Application.Features.Auth.Models;

public sealed record AuthInfoDto
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
