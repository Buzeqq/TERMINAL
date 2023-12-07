namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidProjectNameException : TerminalException
{
    public InvalidProjectNameException(string name) : base($"Unable to create project name with name {name}!")
    {
    }
}