using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class StepConfiguration : IEntityTypeConfiguration<BaseStep>
{
    public void Configure(EntityTypeBuilder<BaseStep> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(i => i.Value,
                i => new StepId(i));
        builder.Property(s => s.Comment)
            .HasConversion(c => c.Value,
                c => new Comment(c));

        builder.HasMany(s => s.Values)
            .WithMany();

        builder.UseTpcMappingStrategy();
    }
}
