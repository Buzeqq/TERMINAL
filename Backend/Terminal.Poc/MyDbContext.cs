using Microsoft.EntityFrameworkCore;

namespace Terminal.Poc;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    public DbSet<Measurement> Measurements { get; set; }
}