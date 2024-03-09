namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidEmailException(string value) : TerminalException($"Invalid email: {value}");