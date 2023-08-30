using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class ParameterNotFoundException : TerminalException
{
    public ParameterNotFoundException(ParameterName name) : base($"Parameter with name: {name} not found")
    {
    }
}