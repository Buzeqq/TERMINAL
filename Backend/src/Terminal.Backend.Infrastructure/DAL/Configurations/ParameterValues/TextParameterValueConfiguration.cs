using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal sealed class TextParameterValueConfiguration : IEntityTypeConfiguration<TextParameterValue>
{
    public void Configure(EntityTypeBuilder<TextParameterValue> builder)
    {
        builder.Property(p => p.Value)
            .HasColumnName($"{nameof(TextParameterValue)}_Value");
    }
}