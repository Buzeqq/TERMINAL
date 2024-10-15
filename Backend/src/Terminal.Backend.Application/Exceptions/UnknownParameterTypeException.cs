using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class UnknownParameterTypeException : TerminalException
{
    public UnknownParameterTypeException(Parameter parameter) :
        base($"Unknown type of parameter {typeof(Parameter).FullName}")
    {
    }

    public UnknownParameterTypeException(StepParameterValueDto dto) :
        base($"Unknown type of parameter: {dto.ParameterId}")
    {
    }
}