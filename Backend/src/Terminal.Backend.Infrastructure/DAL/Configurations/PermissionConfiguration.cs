using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new PermissionId(x));

        builder
            .Property(x => x.Name)
            .HasConversion(x => x.Value,
                x => new PermissionName(x));

        var permissions = Enum.GetValues<Core.Enums.Permission>()
            .Select(p => new Permission((int)p, p.ToString()));

        builder.HasData(permissions);
    }
}