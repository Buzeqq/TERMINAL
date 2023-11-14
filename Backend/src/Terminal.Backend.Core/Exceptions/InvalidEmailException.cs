namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidEmailException : TerminalException
{
    public InvalidEmailException(string value) : base($"Invalid email: {value}")
    {
    }
}