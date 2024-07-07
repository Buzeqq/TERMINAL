using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record RecipeId
{
    public Guid Value { get; }

    public RecipeId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static RecipeId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(RecipeId id) => id.Value;
    public static implicit operator RecipeId(Guid id) => new(id);
}