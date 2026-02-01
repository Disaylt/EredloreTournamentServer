namespace Infrastructure.Models;

public sealed record ConnectionStringOptions
{
    public string DatingPostgresqlDb { get; init; } = string.Empty;
}
