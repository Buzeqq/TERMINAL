namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidPermissionNameException : TerminalException
{
    public InvalidPermissionNameException(string value) : base($"Invalid permission name: {value}")
    {
    }
}