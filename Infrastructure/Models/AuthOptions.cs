namespace Infrastructure.Models;

public sealed record AuthOptions
{
    public string AuthSecret { get; init; } = string.Empty;
    public string RefreshTokenSecret { get; init; } = string.Empty;
    public int ExpireAccessTokenSeconds { get; init; } = 900;
    public int ExpireRefreshTokenHours { get; init; } = 240;
    public string? ValidAudience { get; init; }
    public string? ValidIssuer { get; init; }
    public bool IsCheckValidAudience { get; init; } = true;
    public bool IsCheckValidIssuer { get; init; } = true;
    public bool IsCheckExpireDate { get; init; } = true;
}
