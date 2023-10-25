using MediatR;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Parameter.ChangeStatus;

internal class ChangeParameterStatusCommandHandler : IRequestHandler<ChangeParameterStatusCommand>
{
    private readonly IParameterRepository _repository;

    public ChangeParameterStatusCommandHandler(IParameterRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeParameterStatusCommand command, CancellationToken ct)
    {
        var (name, status) = command;
        var parameter = await _repository.GetAsync<Core.Entities.Parameters.Parameter>(name, ct);

        if (parameter is null)
        {
            throw new ParameterNotFoundException(name);
        }

        parameter.ChangeStatus(status);
        await _repository.UpdateAsync(parameter);
    }
}