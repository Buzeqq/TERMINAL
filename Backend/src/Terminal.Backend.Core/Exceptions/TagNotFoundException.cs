using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Exceptions;

public sealed class TagNotFoundException : TerminalException
{
    public TagNotFoundException(TagId id) : base($"Tag with id: {id} not found")
    {
    }
}