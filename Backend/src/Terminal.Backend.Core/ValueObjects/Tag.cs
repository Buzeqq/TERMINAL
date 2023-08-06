using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record Tag
{
    public string Value { get; }
    public bool IsActive { get; }
    
    public Tag() { }

    public Tag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidTagException(value);
        }

        Value = value;
    }

    public static implicit operator string(Tag tag) => tag.Value;
    public static implicit operator Tag(string value) => new(value);
}