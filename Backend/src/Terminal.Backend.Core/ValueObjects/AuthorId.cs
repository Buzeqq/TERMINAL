using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record AuthorId
{
    public Guid Value { get; }

    public AuthorId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static AuthorId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(AuthorId id) => id.Value;
    public static implicit operator AuthorId(Guid id) => new(id);
}
