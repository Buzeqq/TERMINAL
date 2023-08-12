using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.Parameters;

internal sealed class IntegerParameterConfiguration : IEntityTypeConfiguration<IntegerParameter>
{
    public void Configure(EntityTypeBuilder<IntegerParameter> builder)
    {
        builder.Property(p => p.Step)
            .HasColumnName($"{nameof(IntegerParameter)}_Step");
    }
}