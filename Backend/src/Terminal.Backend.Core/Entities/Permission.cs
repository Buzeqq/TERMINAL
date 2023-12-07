using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Permission
{
    public PermissionId Id { get; private set; }
    public PermissionName Name { get; private set; }

    public Permission(PermissionId id, PermissionName name)
    {
        Id = id;
        Name = name;
    }
}