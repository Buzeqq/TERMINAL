using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record StepId
{
    public Guid Value { get; }

    public StepId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static StepId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(StepId id) => id.Value;
    public static implicit operator StepId(Guid id) => new(id);
}