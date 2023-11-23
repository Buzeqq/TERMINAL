namespace Terminal.Backend.Core.Exceptions;

public class RoleNotFoundException : TerminalException
{
    public RoleNotFoundException(string role) : base($"Role: {role}, not found!")
    {
    }
}