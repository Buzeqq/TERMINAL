using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Repositories;

namespace Terminal.Backend.Application.Commands.Handlers;

public sealed class CreateParameterCommandHandler : ICommandHandler<CreateParameterCommand>
{
    private readonly IParameterRepository _parameterRepository;

    public CreateParameterCommandHandler(IParameterRepository parameterRepository)
    {
        _parameterRepository = parameterRepository;
    }

    public async Task HandleAsync(CreateParameterCommand command, CancellationToken ct)
    {
        var parameter = command.Parameter;

        switch (parameter)
        {
            case TextParameter textParameter:
                await _parameterRepository.AddTextAsync(textParameter, ct);
                break;
            case IntegerParameter integerParameter:
                await _parameterRepository.AddIntegerAsync(integerParameter, ct);
                break;
            case DecimalParameter decimalParameter:
                await _parameterRepository.AddDecimalAsync(decimalParameter, ct);
                break;
            default:
                throw new UnknownParameterTypeException(parameter);
        }
    }
}