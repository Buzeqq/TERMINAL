using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IMeasurementRepository
{
    Task AddAsync(Measurement measurement, CancellationToken ct);
}