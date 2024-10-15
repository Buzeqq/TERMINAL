using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ParameterId
{
    public Guid Value { get; }

    public ParameterId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static ParameterId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ParameterId id) => id.Value;
    public static implicit operator ParameterId(Guid id) => new(id);
}
