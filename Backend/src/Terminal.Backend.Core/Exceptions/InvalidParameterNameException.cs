namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidParameterNameException : TerminalException
{
    public InvalidParameterNameException(string name) : base($"Unable to create parameter with name: [{name}]") { }
}