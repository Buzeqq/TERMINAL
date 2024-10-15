using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(r => r.Value,
                g => new RecipeId(g));
        builder.Property(r => r.Name)
            .HasConversion(n => n.Value,
                n => new RecipeName(n));

        builder.HasMany(r => r.Steps)
            .WithOne();

        // search index
        builder.HasIndex(r => r.Name)
            .HasMethod("GIN")
            .IsTsVectorExpressionIndex("english");
    }
}
