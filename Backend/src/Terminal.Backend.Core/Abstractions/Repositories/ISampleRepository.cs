using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface ISampleRepository
{
    Task AddAsync(Sample sample, CancellationToken ct);
}