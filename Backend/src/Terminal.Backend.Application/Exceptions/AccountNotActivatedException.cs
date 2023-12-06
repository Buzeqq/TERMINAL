using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class AccountNotActivatedException : TerminalException
{
    public AccountNotActivatedException() : base("Account not activated yet!")
    {
    }
}