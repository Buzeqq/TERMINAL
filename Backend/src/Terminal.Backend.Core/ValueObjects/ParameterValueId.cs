using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record ParameterValueId
{
    public Guid Value { get; }

    public ParameterValueId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static ParameterValueId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ParameterValueId id) => id.Value;
    public static implicit operator ParameterValueId(Guid id) => new(id);
}
