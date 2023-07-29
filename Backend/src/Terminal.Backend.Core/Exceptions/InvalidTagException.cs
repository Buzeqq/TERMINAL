namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidTagException : TerminalException
{
    public InvalidTagException(string tag) : base($"Unable to create tag with value: [{tag}].") { }
}