namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidPasswordException : TerminalException
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
}