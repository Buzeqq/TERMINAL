using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.Parameters;

internal sealed class NumericParameterConfiguration : IEntityTypeConfiguration<NumericParameter>
{
    public void Configure(EntityTypeBuilder<NumericParameter> builder)
    {
        builder.Property(p => p.Unit)
            .HasColumnName($"{nameof(NumericParameter)}_Unit");
    }
}