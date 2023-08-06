using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.HasKey(p => p.Name);
        builder
            .Property(p => p.Name)
            .HasConversion(n => n.Value, 
                n => new ParameterName(n));

        builder.HasMany(p => p.ParameterValues)
            .WithOne();

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<IntegerParameter>(nameof(IntegerParameter))
            .HasValue<DecimalParameter>(nameof(DecimalParameter))
            .HasValue<TextParameter>(nameof(TextParameter));
    }
}