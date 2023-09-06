using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

internal sealed class CreateParameterCommandHandler : ICommandHandler<CreateParameterCommand>
{
    private readonly IParameterRepository _parameterRepository;

    public CreateParameterCommandHandler(IParameterRepository parameterRepository)
    {
        _parameterRepository = parameterRepository;
    }

    public async Task HandleAsync(CreateParameterCommand command, CancellationToken ct)
    {
        var parameter = command.Parameter;

        await _parameterRepository.AddAsync(parameter, ct);
    }
}