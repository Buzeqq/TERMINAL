using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder) =>
        builder
            .HasOne(s => s.Recipe)
            .WithMany(r => r.Steps)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
}