using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Value);

        builder
            .HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder
            .Property(x => x.Value)
            .HasConversion(x => x.Value,
                x => new RoleId(x));

        builder
            .HasMany(x => x.Users)
            .WithOne(x => x.Role);

        builder.HasData(Role.GetValues());
    }
}