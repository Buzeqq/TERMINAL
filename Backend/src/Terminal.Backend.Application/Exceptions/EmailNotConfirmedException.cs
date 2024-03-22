namespace Terminal.Backend.Application.Exceptions;

using Terminal.Backend.Core.Exceptions;

public class EmailNotConfirmedException(string action)
    : TerminalException("Email must be confirmed to perform this action", action);
