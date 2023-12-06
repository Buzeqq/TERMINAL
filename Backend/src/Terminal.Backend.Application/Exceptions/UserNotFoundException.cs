using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Exceptions;

internal sealed class UserNotFoundException : TerminalException
{
    public UserNotFoundException() : base("User not found!")
    {
    }
}