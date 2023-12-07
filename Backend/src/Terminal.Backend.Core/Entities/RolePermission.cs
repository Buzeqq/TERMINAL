using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class RolePermission
{
    public RolePermission(PermissionId permissionId, RoleId roleId)
    {
        PermissionId = permissionId;
        RoleId = roleId;
    }

    public PermissionId PermissionId { get; private set; }
    public RoleId RoleId { get; private set; }
}