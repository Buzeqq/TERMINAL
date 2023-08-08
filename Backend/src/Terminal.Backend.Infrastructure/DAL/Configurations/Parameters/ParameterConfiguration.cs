using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.Parameters;

internal sealed class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.HasKey(p => p.Name);
        builder
            .Property(p => p.Name)
            .HasConversion(n => n.Value, 
                n => new ParameterName(n));

        builder.HasMany<ParameterValue>()
            .WithOne(pv => pv.Parameter)
            .HasForeignKey("ParameterName")
            .IsRequired();

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<NumericParameter>(nameof(NumericParameter))
            .HasValue<TextParameter>(nameof(TextParameter))
            .HasValue<IntegerParameter>(nameof(IntegerParameter))
            .HasValue<DecimalParameter>(nameof(DecimalParameter));
    }
}