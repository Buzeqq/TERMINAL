using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record TagId
{
    public Guid Value { get; }

    public TagId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static TagId Create() => new(Guid.NewGuid());
    public static implicit operator Guid(TagId id) => id.Value;
    public static implicit operator TagId(Guid id) => new(id);
}
