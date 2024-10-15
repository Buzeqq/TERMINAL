using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal class ParameterValueConfiguration : IEntityTypeConfiguration<ParameterValue>
{
    public void Configure(EntityTypeBuilder<ParameterValue> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, v => new ParameterValueId(v));
        builder.Ignore(v => v.Parameter);

        builder.HasDiscriminator<string>("parameter_type")
            .HasValue(typeof(TextParameterValue), "text")
            .HasValue(typeof(DecimalParameterValue), "decimal")
            .HasValue(typeof(IntegerParameterValue), "integer");
    }
}
