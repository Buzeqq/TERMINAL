using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record SampleId
{
    public Guid Value { get; }

    public SampleId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static SampleId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(SampleId id) => id.Value;
    public static implicit operator SampleId(Guid id) => new(id);
}
