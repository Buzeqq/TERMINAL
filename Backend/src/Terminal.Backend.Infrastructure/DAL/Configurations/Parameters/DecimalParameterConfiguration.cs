using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.Parameters;

internal sealed class DecimalParameterConfiguration : IEntityTypeConfiguration<DecimalParameter>
{
    public void Configure(EntityTypeBuilder<DecimalParameter> builder)
    {
        builder.Property(p => p.Step)
            .HasColumnName($"{nameof(DecimalParameter)}_Step");
    }
}