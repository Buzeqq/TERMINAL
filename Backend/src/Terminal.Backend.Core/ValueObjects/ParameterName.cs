using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ParameterName
{
    public string Value { get; }

    public ParameterName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator string(ParameterName name) => name.Value;
    public static implicit operator ParameterName(string value) => new(value);
}