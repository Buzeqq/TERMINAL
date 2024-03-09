using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Core.Exceptions;

public sealed class DecimalParameterValueNotValidException(DecimalParameter parameter, decimal value)
    : TerminalException($"The value: {value} is not valid for parameter: {parameter.Name}. " +
                        $"The value must be a multiple of {parameter.Step}.");