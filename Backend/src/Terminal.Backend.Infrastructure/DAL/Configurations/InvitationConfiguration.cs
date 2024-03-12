using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);

        builder
            .Property(i => i.Id)
            .HasConversion(i => i.Value,
                i => new InvitationId(i));

        builder
            .Property(i => i.Link)
            .HasConversion(l => l.Value,
                l => new InvitationLink(l));

        builder
            .HasOne(i => i.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}