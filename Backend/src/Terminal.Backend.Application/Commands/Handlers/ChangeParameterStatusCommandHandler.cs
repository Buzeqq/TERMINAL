using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public class ChangeParameterStatusCommandHandler : ICommandHandler<ChangeParameterStatusCommand>
{
    private readonly IParameterRepository _repository;

    public ChangeParameterStatusCommandHandler(IParameterRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(ChangeParameterStatusCommand command, CancellationToken ct)
    {
        var (name, status) = command;
        var parameter = await _repository.GetAsync(name, ct);

        if (parameter is null)
        {
            throw new ParameterNotFoundException(name);
        }

        parameter.ChangeStatus(status);
        await _repository.UpdateAsync(parameter);
    }
}