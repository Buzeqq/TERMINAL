using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ISampleRepository
{
    Task AddAsync(Sample sample, CancellationToken ct);
    Task<Sample?> GetAsync(SampleId id, CancellationToken cancellationToken);
    Task DeleteAsync(Sample sample, CancellationToken cancellationToken);
    Task UpdateAsync(Sample sample, CancellationToken cancellationToken);
}