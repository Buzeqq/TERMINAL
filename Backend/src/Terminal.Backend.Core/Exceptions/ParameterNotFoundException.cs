using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class ParameterNotFoundException : TerminalException
{
    public ParameterNotFoundException(ParameterId id) : base($"Parameter with id: {id.Value:D} not found")
    {
    }
}