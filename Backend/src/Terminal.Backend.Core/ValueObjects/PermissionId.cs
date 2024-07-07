using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record PermissionId
{
    public int Value { get; }

    public PermissionId(int value)
    {
        if (!Enum.IsDefined(typeof(Permission), value))
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator int(PermissionId id) => id.Value;
    public static implicit operator PermissionId(int id) => new(id);
}