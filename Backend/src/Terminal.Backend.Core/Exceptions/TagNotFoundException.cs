using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class TagNotFoundException : TerminalException
{
    public TagNotFoundException(TagName name) : base($"Tag with name: {name} not found")
    {
    }
}