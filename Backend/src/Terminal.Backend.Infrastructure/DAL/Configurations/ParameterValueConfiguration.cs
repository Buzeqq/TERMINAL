using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

public class ParameterValueConfiguration : IEntityTypeConfiguration<ParameterValue>
{
    public void Configure(EntityTypeBuilder<ParameterValue> builder)
    {
        builder.HasKey(p => p.Id);
    }
}