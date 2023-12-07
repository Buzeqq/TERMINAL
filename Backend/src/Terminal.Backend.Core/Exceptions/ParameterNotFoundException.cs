namespace Terminal.Backend.Core.Exceptions;

public sealed class ParameterNotFoundException : TerminalException
{
    public ParameterNotFoundException() : base("Parameter not found")
    {
    }
}