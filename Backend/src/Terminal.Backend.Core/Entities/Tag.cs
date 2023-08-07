using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.Entities;

public sealed class Tag
{
    public string Value { get; private set; }
    public bool IsActive { get; private set; }
    
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