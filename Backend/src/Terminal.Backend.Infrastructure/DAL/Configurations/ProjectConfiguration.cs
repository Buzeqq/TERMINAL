using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(i => i.Value, i => new ProjectId(i));
        builder.Property(p => p.Name)
            .HasConversion(n => n.Value, n => new ProjectName(n));

        builder.HasIndex(p => p.Name).IsUnique();
        builder.HasMany(p => p.Samples)
            .WithOne(m => m.Project)
            .HasForeignKey("ProjectId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasQueryFilter(p => p.IsActive);
    }
}
