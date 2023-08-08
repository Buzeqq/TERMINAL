using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal sealed class IntegerParameterValueConfiguration : IEntityTypeConfiguration<IntegerParameterValue>
{
    public void Configure(EntityTypeBuilder<IntegerParameterValue> builder)
    {
        builder.Property(p => p.Value)
            .HasColumnName($"{nameof(IntegerParameterValue)}_Value");
    }
}