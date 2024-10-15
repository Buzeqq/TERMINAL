using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class SampleRepository(TerminalDbContext dbContext) : ISampleRepository
{
    private readonly DbSet<Sample> _samples = dbContext.Samples;

    public async Task AddAsync(Sample sample, CancellationToken cancellationToken)
        => await _samples.AddAsync(sample, cancellationToken);

    public Task<Sample?> GetAsync(SampleId id, CancellationToken cancellationToken)
        =>
            _samples
            .Include(s => s.Project)
            .Include(s => s.Recipe)
            .Include(s => s.Steps)
            .ThenInclude(s => s.Values)
            .ThenInclude(p => p.Parameter)
            .Include(s => s.Tags)
            .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task<bool> ExistsAsync(SampleId id, CancellationToken cancellationToken)
        => _samples.AnyAsync(s => s.Id == id, cancellationToken);

    public Task DeleteAsync(SampleId sample, CancellationToken cancellationToken) =>
        _samples
            .Where(s => s.Id == sample)
            .ExecuteDeleteAsync(cancellationToken);

    public Task UpdateAsync(Sample sample, CancellationToken cancellationToken)
    {
        _samples.Update(sample);
        return Task.CompletedTask;
    }
}
