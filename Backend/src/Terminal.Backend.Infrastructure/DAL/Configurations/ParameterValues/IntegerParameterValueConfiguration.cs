using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Configurations.ParameterValues;

internal class IntegerParameterValueConfiguration : IEntityTypeConfiguration<IntegerParameterValue>
{
    private static string Prefix => "integer";

    public void Configure(EntityTypeBuilder<IntegerParameterValue> builder)
    {
        builder.Property(v => v.Value)
            .HasColumnName($"{Prefix}_value");

        builder.HasOne(v => v.IntegerParameter)
            .WithMany();

        builder.Navigation(v => v.IntegerParameter).AutoInclude();
    }
}
