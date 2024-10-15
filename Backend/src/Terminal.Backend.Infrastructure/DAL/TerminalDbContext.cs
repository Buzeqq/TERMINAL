using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL;

internal sealed class TerminalDbContext(DbContextOptions<TerminalDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Sample> Samples { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<SampleStep> SampleSteps { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<IntegerParameter> IntegerParameters { get; set; }
    public DbSet<DecimalParameter> DecimalParameters { get; set; }
    public DbSet<TextParameter> TextParameters { get; set; }
    public DbSet<NumericParameter> NumericParameters { get; set; }
    public DbSet<ParameterValue> ParameterValues { get; set; }
    public DbSet<IntegerParameterValue> IntegerParameterValues { get; set; }
    public DbSet<DecimalParameterValue> DecimalParameterValues { get; set; }
    public DbSet<TextParameterValue> TextParameterValues { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql()
            .UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("data");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
