using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Exceptions;

internal sealed class UserNotFoundException : TerminalException
{
    public UserNotFoundException(Email email) : base($"User with email: {email.Value} not found!")
    {
    }
}