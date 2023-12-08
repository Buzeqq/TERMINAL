using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Role : Enumeration<Role, RoleId>
{
    public static readonly Role Administrator = new(new RoleId(3), nameof(Administrator));
    public static readonly Role Moderator = new(new RoleId(2), nameof(Moderator));
    public static readonly Role Registered = new(new RoleId(1), nameof(Registered));
    public static readonly Role Guest = new(new RoleId(0), nameof(Guest));

    private Role(RoleId value, string name) : base(value, name)
    {
    }

    public ICollection<Permission> Permissions { get; private set; } = new List<Permission>();
    public ICollection<User> Users { get; private set; } = new List<User>();

    public static bool IsSatisfied(Role minimalRole, Role userRole)
        => minimalRole.Value.Value <= userRole.Value.Value;
}