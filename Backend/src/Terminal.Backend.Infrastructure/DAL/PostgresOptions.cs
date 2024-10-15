namespace Terminal.Backend.Infrastructure.DAL;

internal sealed class PostgresOptions
{
    public string ConnectionString { get; init; } = string.Empty;
    public bool Seed { get; init; }
}
