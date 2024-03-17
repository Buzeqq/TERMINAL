using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

public sealed class LoginFailedException(string details) : TerminalException("Login failed", details);