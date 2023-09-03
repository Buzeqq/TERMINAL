using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Core.Repositories;

public interface IParameterValueRepository
{
    Task AddAsync(ParameterValue value, CancellationToken ct);
}