using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Core.Exceptions;

public sealed class DecimalParameterValueNotValidException : TerminalException
{
    public DecimalParameterValueNotValidException(DecimalParameter parameter, decimal value)
        : base($"The value: {value} is not valid for parameter: {parameter.Name}. " +
               $"The value must be a multiple of {parameter.Step}.")
    {
    }
}