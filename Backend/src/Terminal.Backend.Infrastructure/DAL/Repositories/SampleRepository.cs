using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

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

    public Task<Sample?> GetAsync(SampleId id, CancellationToken cancellationToken)
        => _samples
            .Include(s => s.Project)
            .Include(s => s.Recipe)
            .Include(s => s.Steps)
            .ThenInclude(s => s.Parameters)
            .ThenInclude(p => p.Parameter)
            .Include(s => s.Tags)
            .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task DeleteAsync(Sample sample, CancellationToken cancellationToken)
    {
        _samples.Remove(sample);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Sample sample, CancellationToken cancellationToken)
    {
        _samples.Update(sample);
        return Task.CompletedTask;
    }
}