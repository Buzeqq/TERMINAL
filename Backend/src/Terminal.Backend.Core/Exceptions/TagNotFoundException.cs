namespace Terminal.Backend.Core.Exceptions;

public sealed class TagNotFoundException : TerminalException
{
    public TagNotFoundException() : base("Tag not found")
    {
    }
}