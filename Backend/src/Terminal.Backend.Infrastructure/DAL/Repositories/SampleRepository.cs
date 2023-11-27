using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class SampleRepository : ISampleRepository
{
    private readonly DbSet<Sample> _samples;

    public SampleRepository(TerminalDbContext dbContext)
    {
        _samples = dbContext.Samples;
    }

    public async Task AddAsync(Sample sample, CancellationToken ct)
        => await _samples.AddAsync(sample, ct);
}