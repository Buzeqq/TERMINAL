using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.DAL.ValueGenerators;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(i => i.Value,
                i => new MeasurementId(i));
        builder.Property(m => m.Code)
            .HasConversion(c => c.Value,
                c => new MeasurementCode(c))
            .HasValueGenerator(typeof(MeasurementCodeValueGenerator));
        builder.Property(m => m.Comment)
            .HasConversion(c => c.Value, 
                c => new Comment(c));

        builder.HasMany(m => m.Steps)
            .WithOne()
            .IsRequired();
        builder.HasMany(m => m.Tags)
            .WithOne();
        builder.HasOne(m => m.Recipe)
            .WithMany()
            .HasForeignKey("RecipeId")
            .IsRequired(false);
    }
}