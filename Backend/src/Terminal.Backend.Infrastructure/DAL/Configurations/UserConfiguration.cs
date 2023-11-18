using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.Administrator;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value,
                x => new UserId(x));

        builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value,
                x => new Email(x))
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasConversion(x => x.Value,
                x => new Password(x))
            .IsRequired();

        builder
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}