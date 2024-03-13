using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class FailedToRegisterUserException(string details) 
    : TerminalException("Failed to register user", details);