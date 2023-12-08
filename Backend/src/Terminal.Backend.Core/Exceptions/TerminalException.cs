namespace Terminal.Backend.Core.Exceptions;

public abstract class TerminalException : Exception
{
    protected TerminalException(string message) : base(message)
    {
    }
}