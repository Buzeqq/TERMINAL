using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Exceptions;

internal sealed class UserNotFoundException() : TerminalException("User not found!");