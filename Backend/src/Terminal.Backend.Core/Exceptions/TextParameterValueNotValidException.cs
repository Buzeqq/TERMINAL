using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Core.Exceptions;

public sealed class TextParameterValueNotValidException : TerminalException
{
    public TextParameterValueNotValidException(TextParameter parameter, string value)
        : base($"Value: {value} is not valid for parameter: {parameter.Name}. " +
               $"Valid values are: {parameter.AllowedValues}")
    {
    }
}