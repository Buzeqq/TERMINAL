namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidProjectNameException(string name)
    : TerminalException($"Unable to create project name with name {name}!");
