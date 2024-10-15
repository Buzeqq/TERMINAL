using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal class DecimalParameterValueConfiguration : IEntityTypeConfiguration<DecimalParameterValue>
{
    private static string Prefix => "decimal";

    public void Configure(EntityTypeBuilder<DecimalParameterValue> builder)
    {
        builder.Property(v => v.Value)
            .HasColumnName($"{Prefix}_value");

        builder.HasOne(v => v.DecimalParameter)
            .WithMany();

        builder.Navigation(v => v.DecimalParameter).AutoInclude();
    }
}
