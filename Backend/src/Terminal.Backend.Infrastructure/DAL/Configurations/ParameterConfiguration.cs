using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(p => p.Value,
                p => new ParameterId(p));

        builder.Property(p => p.ParentId)
            .HasConversion(p => p!.Value,
                p => new ParameterId(p));

        builder.Property(p => p.Name)
            .HasConversion(n => n.Value,
                n => new ParameterName(n));

        builder.UseTphMappingStrategy();

        builder.HasOne(p => p.Parent)
            .WithMany()
            .HasForeignKey(p => p.ParentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(p => p.IsActive);
    }
}
