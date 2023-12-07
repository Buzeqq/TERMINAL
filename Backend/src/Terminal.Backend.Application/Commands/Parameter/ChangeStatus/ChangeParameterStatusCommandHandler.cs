using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

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
        var (id, status) = command;
        var parameter = await _repository.GetAsync<Core.Entities.Parameters.Parameter>(id, ct);

        if (parameter is null)
        {
            throw new ParameterNotFoundException();
        }

        parameter.ChangeStatus(status);
        await _repository.UpdateAsync(parameter);
    }
}