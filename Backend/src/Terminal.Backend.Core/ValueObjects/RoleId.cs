namespace Terminal.Backend.Core.ValueObjects;

public sealed record RoleId(int Value)
{
    public static implicit operator int(RoleId id) => id.Value;
    public static implicit operator RoleId(int id) => new(id);
}
