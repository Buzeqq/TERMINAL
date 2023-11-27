using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
{
    public void Configure(EntityTypeBuilder<RecipeStep> builder)
    {
        // https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#table-per-concrete-type-configuration
        // separate table for recipe step, to make sure update of recipe steps, won't override samples created with
        // recipe
    }
}