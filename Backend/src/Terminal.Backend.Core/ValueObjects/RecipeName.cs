using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record RecipeName
{
    public string Name { get; }

    public RecipeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidRecipeNameException(name);
        }

        Name = name;
    }

    public static implicit operator string(RecipeName name) => name.Name;
    public static implicit operator RecipeName(string value) => new(value);
}