using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Repositories;

public interface IParameterRepository
{
    Task<Parameter?> GetAsync(ParameterName name, CancellationToken ct);
    Task AddTextAsync(TextParameter parameter, CancellationToken ct);
    Task AddIntegerAsync(IntegerParameter parameter, CancellationToken ct);
    Task AddDecimalAsync(DecimalParameter parameter, CancellationToken ct);
    Task UpdateAsync(Parameter parameter);
}