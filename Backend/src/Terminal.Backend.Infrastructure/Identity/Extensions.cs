using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Infrastructure.Identity;

internal static class Extensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>((o) =>
            {
                o.User.RequireUniqueEmail = true;
            })
            .AddRoles<ApplicationRole>()
            .AddSignInManager()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<UserDbContext>();

        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                o.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            })
            .AddBearerToken(IdentityConstants.BearerScheme);
    }
}
