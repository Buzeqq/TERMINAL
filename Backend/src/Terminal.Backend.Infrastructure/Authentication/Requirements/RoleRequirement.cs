using Microsoft.AspNetCore.Authorization;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.Authentication.Requirements;

public sealed class RoleRequirement : IAuthorizationRequirement
{
    public RoleRequirement(Role role)
    {
        Role = role;
    }

    public Role Role { get; set; }
}