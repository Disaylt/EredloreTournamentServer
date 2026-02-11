namespace Infrastructure.Models;

public sealed record ConnectionStringOptions
{
    public string Redis { get; init; } = string.Empty;
    public string GamePostgresqlDb { get; init; } = string.Empty;
}
