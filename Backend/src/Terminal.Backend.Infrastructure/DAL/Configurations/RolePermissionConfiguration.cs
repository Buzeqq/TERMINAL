using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;
using Permission = Terminal.Backend.Core.Enums.Permission;

namespace Terminal.Backend.Infrastructure.DAL.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.PermissionId, x.RoleId });

        builder
            .Property(x => x.PermissionId)
            .HasConversion(x => x.Value,
                x => new PermissionId(x));

        builder
            .Property(x => x.RoleId)
            .HasConversion(x => x.Value,
                x => new RoleId(x));

        builder.HasData(Create(Role.Administrator, Permission.UserRead),
            Create(Role.Administrator, Permission.UserWrite), Create(Role.Administrator, Permission.UserUpdate),
            Create(Role.Administrator, Permission.UserDelete), Create(Role.Administrator, Permission.ProjectRead),
            Create(Role.Administrator, Permission.ProjectWrite), Create(Role.Administrator, Permission.ProjectUpdate),
            Create(Role.Administrator, Permission.ProjectDelete), Create(Role.Administrator, Permission.RecipeRead),
            Create(Role.Administrator, Permission.RecipeWrite), Create(Role.Administrator, Permission.RecipeUpdate),
            Create(Role.Administrator, Permission.RecipeDelete), Create(Role.Administrator, Permission.TagRead),
            Create(Role.Administrator, Permission.TagWrite), Create(Role.Administrator, Permission.TagUpdate),
            Create(Role.Administrator, Permission.TagDelete), Create(Role.Administrator, Permission.SampleRead),
            Create(Role.Administrator, Permission.SampleWrite),
            Create(Role.Administrator, Permission.SampleUpdate),
            Create(Role.Administrator, Permission.SampleDelete),
            Create(Role.Administrator, Permission.ParameterRead), Create(Role.Administrator, Permission.ParameterWrite),
            Create(Role.Administrator, Permission.ParameterUpdate),
            Create(Role.Administrator, Permission.ParameterDelete), Create(Role.Administrator, Permission.StepRead),
            Create(Role.Administrator, Permission.StepWrite), Create(Role.Administrator, Permission.StepUpdate),
            Create(Role.Administrator, Permission.StepDelete), Create(Role.Moderator, Permission.UserRead),
            Create(Role.Moderator, Permission.ProjectRead), Create(Role.Moderator, Permission.ProjectWrite),
            Create(Role.Moderator, Permission.ProjectUpdate), Create(Role.Moderator, Permission.ProjectDelete),
            Create(Role.Moderator, Permission.RecipeRead), Create(Role.Moderator, Permission.RecipeWrite),
            Create(Role.Moderator, Permission.RecipeUpdate), Create(Role.Moderator, Permission.RecipeDelete),
            Create(Role.Moderator, Permission.TagRead), Create(Role.Moderator, Permission.TagWrite),
            Create(Role.Moderator, Permission.TagUpdate), Create(Role.Moderator, Permission.TagDelete),
            Create(Role.Moderator, Permission.SampleRead), Create(Role.Moderator, Permission.SampleWrite),
            Create(Role.Moderator, Permission.SampleUpdate), Create(Role.Moderator, Permission.SampleDelete),
            Create(Role.Moderator, Permission.ParameterRead), Create(Role.Moderator, Permission.ParameterWrite),
            Create(Role.Moderator, Permission.ParameterUpdate), Create(Role.Moderator, Permission.ParameterDelete),
            Create(Role.Moderator, Permission.StepRead), Create(Role.Moderator, Permission.StepWrite),
            Create(Role.Moderator, Permission.StepUpdate), Create(Role.Moderator, Permission.StepDelete),
            Create(Role.Registered, Permission.ProjectRead), Create(Role.Registered, Permission.RecipeRead),
            Create(Role.Registered, Permission.TagRead), Create(Role.Registered, Permission.TagWrite),
            Create(Role.Registered, Permission.TagUpdate), Create(Role.Registered, Permission.SampleRead),
            Create(Role.Registered, Permission.SampleWrite), Create(Role.Registered, Permission.SampleUpdate),
            Create(Role.Registered, Permission.ParameterRead), Create(Role.Registered, Permission.StepRead),
            Create(Role.Registered, Permission.StepWrite), Create(Role.Registered, Permission.StepUpdate));
    }

    private static RolePermission Create(
        Role role, Permission permission)
        => new(new PermissionId((int)permission), role.Value);
}