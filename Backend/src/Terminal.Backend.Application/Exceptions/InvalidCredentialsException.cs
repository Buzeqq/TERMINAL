using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal sealed class InvalidCredentialsException() : TerminalException("Invalid credentials");