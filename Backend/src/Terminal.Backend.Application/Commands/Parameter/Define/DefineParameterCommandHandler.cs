using MediatR;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Parameter.Define;

internal sealed class DefineParameterCommandHandler : IRequestHandler<DefineParameterCommand>
{
    private readonly IParameterRepository _parameterRepository;

    public DefineParameterCommandHandler(IParameterRepository parameterRepository)
    {
        _parameterRepository = parameterRepository;
    }

    public Task Handle(DefineParameterCommand command, CancellationToken ct)
    {
        var parameter = command.Parameter;

        return _parameterRepository.AddAsync(parameter, ct);
    }
}