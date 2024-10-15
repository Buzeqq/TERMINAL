using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Core.Enums;

namespace Terminal.Backend.Infrastructure.Authorization;

internal static class Extensions
{
    public static void AddTerminalAuthorization(this IServiceCollection services) =>
        services.AddAuthorizationBuilder()
            .AddRole(ApplicationRole.Administrator)
            .AddRole(ApplicationRole.Moderator)
            .AddRole(ApplicationRole.User)
            .AddRole(ApplicationRole.Guest)
            .AddPermission(Permission.UserRead)
            .AddPermission(Permission.UserWrite)
            .AddPermission(Permission.UserUpdate)
            .AddPermission(Permission.UserDelete)
            .AddPermission(Permission.ParameterRead)
            .AddPermission(Permission.ParameterWrite)
            .AddPermission(Permission.ParameterUpdate)
            .AddPermission(Permission.ParameterDelete)
            .AddPermission(Permission.ProjectRead)
            .AddPermission(Permission.ProjectWrite)
            .AddPermission(Permission.ProjectUpdate)
            .AddPermission(Permission.ProjectDelete)
            .AddPermission(Permission.RecipeRead)
            .AddPermission(Permission.RecipeWrite)
            .AddPermission(Permission.RecipeUpdate)
            .AddPermission(Permission.RecipeDelete)
            .AddPermission(Permission.TagRead)
            .AddPermission(Permission.TagWrite)
            .AddPermission(Permission.TagUpdate)
            .AddPermission(Permission.TagDelete)
            .AddPermission(Permission.ParameterRead)
            .AddPermission(Permission.ParameterWrite)
            .AddPermission(Permission.ParameterUpdate)
            .AddPermission(Permission.ParameterDelete)
            .AddPermission(Permission.SampleRead)
            .AddPermission(Permission.SampleWrite)
            .AddPermission(Permission.SampleUpdate)
            .AddPermission(Permission.SampleDelete)
            .AddPermission(Permission.StepRead)
            .AddPermission(Permission.StepWrite)
            .AddPermission(Permission.StepUpdate)
            .AddPermission(Permission.StepDelete);

    private static AuthorizationBuilder AddRole(this AuthorizationBuilder builder, ApplicationRole role)
        => builder.AddPolicy(role.Name!, policy => policy.RequireRole(role.Name!));
    private static AuthorizationBuilder AddPermission(this AuthorizationBuilder builder, Permission permission)
        => builder.AddPolicy(permission.ToString(), policy => policy.RequireClaim(permission.ToString(), "true"));

}
