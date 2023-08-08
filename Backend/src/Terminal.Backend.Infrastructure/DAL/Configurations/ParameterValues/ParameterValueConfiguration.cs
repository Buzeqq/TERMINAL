using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal sealed class ParameterValueConfiguration : IEntityTypeConfiguration<ParameterValue>
{
    public void Configure(EntityTypeBuilder<ParameterValue> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(i => i.Value,
                i => new ParameterValueId(i));
    }
}