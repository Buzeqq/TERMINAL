using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal sealed class DecimalParameterValueConfiguration : IEntityTypeConfiguration<DecimalParameterValue>
{
    public void Configure(EntityTypeBuilder<DecimalParameterValue> builder)
    {
        builder.Property(p => p.Value)
            .HasColumnName($"{nameof(DecimalParameterValue)}_Value");
    }
}