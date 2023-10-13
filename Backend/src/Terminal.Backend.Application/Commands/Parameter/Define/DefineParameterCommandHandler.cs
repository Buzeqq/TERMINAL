using MediatR;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Parameter.Define;

internal sealed class DefineParameterCommandHandler : IRequestHandler<DefineParameterCommand>
{
    private readonly IParameterRepository _parameterRepository;

    public DefineParameterCommandHandler(IParameterRepository parameterRepository)
    {
        _parameterRepository = parameterRepository;
    }

    public async Task Handle(DefineParameterCommand command, CancellationToken ct)
    {
        var parameter = command.Parameter;

        await _parameterRepository.AddAsync(parameter, ct);
    }
}