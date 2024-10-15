using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IParameterRepository
{
    Task<T?> GetAsync<T>(ParameterId id, CancellationToken cancellationToken) where T : Parameter;
    Task AddAsync(Parameter parameter, CancellationToken cancellationToken);
    Task UpdateAsync(Parameter parameter);
}
