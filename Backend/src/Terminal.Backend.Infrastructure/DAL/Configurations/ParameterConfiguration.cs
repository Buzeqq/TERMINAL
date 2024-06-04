using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasConversion(p => p.Value,
                p => new ParameterId(p));

        builder
            .Property(p => p.Name)
            .HasConversion(n => n.Value,
                n => new ParameterName(n));

        builder.HasMany<ParameterValue>()
            .WithOne(pv => pv.Parameter)
            .HasForeignKey("parameter_name")
            .IsRequired();

        builder.HasOne(p => p.Parent)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        builder.UseTpcMappingStrategy();
    }
}