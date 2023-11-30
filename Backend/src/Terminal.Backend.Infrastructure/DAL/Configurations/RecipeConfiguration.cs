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
        builder
            .Property(r => r.Id)
            .HasConversion(r => r.Value,
                g => new RecipeId(g));
        builder
            .Property(r => r.RecipeName)
            .HasConversion(n => n.Name,
                n => new RecipeName(n));

        // search index
        builder.HasIndex(r => r.RecipeName)
            .HasMethod("GIN")
            .IsTsVectorExpressionIndex("english");
    }
}