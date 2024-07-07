using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record TagName
{
    public string Value { get; }

    public TagName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator string(TagName name) => name.Value;
    public static implicit operator TagName(string value) => new(value);
}