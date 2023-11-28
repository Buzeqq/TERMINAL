using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class SampleStepConfiguration : IEntityTypeConfiguration<SampleStep>
{
    public void Configure(EntityTypeBuilder<SampleStep> builder)
    {
    }
}