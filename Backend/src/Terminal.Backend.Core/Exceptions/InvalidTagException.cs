using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class InvalidTagException : TerminalException
{
    public InvalidTagException(TagName tag) : base($"Unable to create tag with name {tag}!")
    {
    }
}