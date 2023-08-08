using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed class MeasurementId
{
    public Guid Value { get; }

    public MeasurementId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidEntityIdException(id);
        }

        Value = id;
    }

    public static MeasurementId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(MeasurementId id) => id.Value;
    public static implicit operator MeasurementId(Guid id) => new(id);
}