namespace Terminal.Backend.Core.ValueObjects;

public sealed record Comment(string Value)
{
    public static implicit operator string(Comment comment) => comment.Value;
    public static implicit operator Comment(string comment) => new(comment);
}