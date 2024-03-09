namespace Terminal.Backend.Core.Exceptions;

public class RoleNotFoundException(string role) : TerminalException($"Role: {role}, not found!");