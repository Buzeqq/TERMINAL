using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal class TextParameterValueConfiguration : IEntityTypeConfiguration<TextParameterValue>
{
    private static string Prefix => "text";

    public void Configure(EntityTypeBuilder<TextParameterValue> builder)
    {
        builder.Property(v => v.Value)
            .HasColumnName($"{Prefix}_value");

        builder.HasOne(v => v.TextParameter)
            .WithMany();

        builder.Navigation(v => v.TextParameter).AutoInclude();
    }
}
