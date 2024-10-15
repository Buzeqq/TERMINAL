using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(s => s.Id)
            .HasConversion(i => i.Value,
                i => new TagId(i));

        builder.Property(t => t.Name)
            .HasConversion(n => n.Value,
                n => new TagName(n));

        builder.HasIndex(t => t.Name).IsUnique();
        builder.HasQueryFilter(t => t.IsActive);
    }
}
