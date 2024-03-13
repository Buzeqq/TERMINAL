namespace Terminal.Backend.Core.Exceptions;

public abstract class TerminalException(string title, string details = "") : Exception(title)
{
    public string Details { get; init; } = details;
    public IEnumerable<string> Errors { get; init; } = [];
}