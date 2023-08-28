using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Core.Repositories;

public interface IParameterRepository
{
    Task AddTextAsync(TextParameter parameter, CancellationToken ct);
    Task AddIntegerAsync(IntegerParameter parameter, CancellationToken ct);
    Task AddDecimalAsync(DecimalParameter parameter, CancellationToken ct);
}