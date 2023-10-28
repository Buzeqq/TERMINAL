using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(i => i.Value,
                i => new ProjectId(i));
        builder.HasIndex(p => p.Name).IsUnique();
        builder.HasMany(p => p.Measurements)
            .WithOne(m => m.Project)
            .HasForeignKey("ProjectId")
            .IsRequired();

        // search index
        // builder
        //     .HasIndex(p => p.Name)
        //     .HasMethod("GIN")
        //     .IsTsVectorExpressionIndex("english");
    }
}