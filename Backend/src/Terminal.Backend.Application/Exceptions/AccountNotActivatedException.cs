using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal class AccountNotActivatedException() : TerminalException("Account not activated yet!");
