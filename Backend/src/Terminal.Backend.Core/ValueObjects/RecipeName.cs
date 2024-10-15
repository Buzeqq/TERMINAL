using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record RecipeName
{
    public string Value { get; }

    public RecipeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidRecipeNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(RecipeName name) => name.Value;
    public static implicit operator RecipeName(string value) => new(value);
}
