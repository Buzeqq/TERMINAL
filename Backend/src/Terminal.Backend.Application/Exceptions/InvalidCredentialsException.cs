using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal sealed class InvalidCredentialsException : TerminalException
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}