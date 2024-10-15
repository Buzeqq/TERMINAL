using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ISampleRepository
{
    Task AddAsync(Sample sample, CancellationToken cancellationToken);
    Task<Sample?> GetAsync(SampleId id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(SampleId id, CancellationToken cancellationToken);
    Task DeleteAsync(SampleId sample, CancellationToken cancellationToken);
    Task UpdateAsync(Sample sample, CancellationToken cancellationToken);
}
