using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Core.ValueObjects;

public sealed record PermissionName
{
    public string Value { get; }

    public PermissionName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPermissionNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(PermissionName name) => name.Value;
    public static implicit operator PermissionName(string name) => new(name);
}