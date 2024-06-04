namespace Terminal.Backend.Application.Exceptions;

using Terminal.Backend.Core.Exceptions;

public class FailedToResetPasswordException()
    : TerminalException("Failed to reset password.");
