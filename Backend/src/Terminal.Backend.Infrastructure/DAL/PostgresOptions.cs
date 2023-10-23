namespace Terminal.Backend.Infrastructure.DAL;

internal sealed class PostgresOptions
{
    public string ConnectionString { get; set; }
    public bool Seed { get; set; }
}