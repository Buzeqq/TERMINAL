using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Parameters.Define;

internal sealed class DefineParameterCommandHandler(IParameterRepository parameterRepository)
    : IRequestHandler<DefineParameterCommand>
{
    public Task Handle(DefineParameterCommand command, CancellationToken ct)
    {
        var parameter = command.Parameter;

        return parameterRepository.AddAsync(parameter, ct);
    }
}