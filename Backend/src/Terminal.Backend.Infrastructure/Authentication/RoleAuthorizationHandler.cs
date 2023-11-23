using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Infrastructure.Authentication.Requirements;

namespace Terminal.Backend.Infrastructure.Authentication;

internal sealed class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var userRole = context.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty;

        var minimalRole = requirement.Role;
        if (Role.IsSatisfied(minimalRole, Role.FromName(userRole) ?? Role.Guest))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}