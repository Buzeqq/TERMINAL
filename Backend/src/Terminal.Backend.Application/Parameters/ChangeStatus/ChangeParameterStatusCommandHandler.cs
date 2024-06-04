using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Parameters.ChangeStatus;

internal class ChangeParameterStatusCommandHandler(IParameterRepository repository)
    : IRequestHandler<ChangeParameterStatusCommand>
{
    public async Task Handle(ChangeParameterStatusCommand command, CancellationToken ct)
    {
        var (id, status) = command;
        var parameter = await repository.GetAsync<Core.Entities.Parameters.Parameter>(id, ct);

        if (parameter is null)
        {
            throw new ParameterNotFoundException();
        }

        parameter.ChangeStatus(status);
        await repository.UpdateAsync(parameter);
    }
}