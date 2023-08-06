using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder
            .Property(s => s.Comment)
            .HasConversion(c => c.Value,
                c => new Comment(c));

        builder
            .HasMany(s => s.Parameters)
            .WithOne()
            .IsRequired();
    }
}