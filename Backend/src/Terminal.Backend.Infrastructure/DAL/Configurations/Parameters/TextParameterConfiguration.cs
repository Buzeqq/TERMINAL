using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.Parameters;

internal sealed class TextParameterConfiguration : IEntityTypeConfiguration<TextParameter>
{
    public void Configure(EntityTypeBuilder<TextParameter> builder)
    {
        builder.Property(p => p.AllowedValues)
            .HasColumnName($"{nameof(TextParameter)}_AllowedValues");

        // builder.Property(p => p.AllowedValues)
        //     .HasConversion(x => JsonSerializer.Serialize(x, JsonSerializerOptions.Default),
        //     x => JsonSerializer.Deserialize<List<string>>(x, JsonSerializerOptions.Default)!);
    }
}