using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Core.Exceptions;

public sealed class TextParameterValueNotValidException(TextParameter parameter, string value) : TerminalException(
    $"Value: {value} is not valid for parameter: {parameter.Name}. " +
    $"Valid values are: {parameter.AllowedValues}");