namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidPermissionNameException(string value)
    : TerminalException($"Invalid permission name: {value}");