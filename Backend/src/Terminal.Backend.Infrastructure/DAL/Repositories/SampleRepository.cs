using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class SampleRepository : ISampleRepository
{
    private readonly DbSet<Sample> _measurements;

    public SampleRepository(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task AddAsync(Sample sample, CancellationToken ct)
        => await _measurements.AddAsync(sample, ct);
}