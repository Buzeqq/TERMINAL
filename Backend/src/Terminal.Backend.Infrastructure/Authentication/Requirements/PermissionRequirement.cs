using Microsoft.AspNetCore.Authorization;

namespace Terminal.Backend.Infrastructure.Authentication.Requirements;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; set; }
}