using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class InvitationNotFoundExceptions : TerminalException
{
    public InvitationNotFoundExceptions() : base("Invitation not found!")
    {
    }
}