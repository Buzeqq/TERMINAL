using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Core.Enums;

namespace Terminal.Backend.Infrastructure.DAL;

internal class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql()
            .UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("users");
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .UseTphMappingStrategy()
            .HasDiscriminator<string>("RoleType")
            .HasValue<ApplicationRole>(nameof(ApplicationRole));

        builder.Entity<ApplicationRole>()
            .ToTable("AspNetRoles", schema: "users")
            .HasData(ApplicationRole.AvailableRoles);

        #region Perrmisions
        builder.Entity<IdentityRoleClaim<string>>()
            .HasData(new IdentityRoleClaim<string>
            {
                Id = 1,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.UserRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 2,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.UserWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 3,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.UserUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 4,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.UserDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 5,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 6,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 7,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 8,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 9,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ParameterRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 10,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ParameterWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 11,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ParameterUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 12,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ParameterDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 13,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 14,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 15,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 16,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.ProjectDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 17,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.TagRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 18,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.TagWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 19,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.TagUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 20,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.TagDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 21,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.RecipeRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 22,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.RecipeWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 23,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.RecipeUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 24,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.RecipeDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 25,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.SampleRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 26,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.SampleWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 27,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.SampleUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 28,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.SampleDelete.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 29,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.StepRead.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 30,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.StepWrite.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 31,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.StepUpdate.ToString(),
                ClaimValue = "true"
            }, new IdentityRoleClaim<string>
            {
                Id = 32,
                RoleId = ApplicationRole.Administrator.Id,
                ClaimType = Permission.StepDelete.ToString(),
                ClaimValue = "true"
            });

        builder.Entity<IdentityRoleClaim<string>>()
            .HasData(
            new IdentityRoleClaim<string>
            {
                Id = 33,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.UserRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 34,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 35,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 36,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 37,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectWrite.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 38,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 39,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ProjectDelete.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 40,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.RecipeRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 41,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.RecipeUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 42,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.RecipeUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 43,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.RecipeDelete.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 44,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.TagRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 45,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.TagWrite.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 46,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.TagUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 47,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.TagDelete.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 48,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.SampleRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 49,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.SampleWrite.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 50,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.SampleUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 51,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.SampleDelete.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 52,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ParameterRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 53,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ParameterWrite.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 54,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ParameterUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 55,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.ParameterDelete.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 56,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.StepRead.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 57,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.StepWrite.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 58,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.StepUpdate.ToString(),
                ClaimValue = "true"
            },
            new IdentityRoleClaim<string>
            {
                Id = 59,
                RoleId = ApplicationRole.Moderator.Id,
                ClaimType = Permission.StepDelete.ToString(),
                ClaimValue = "true"
            });

        builder.Entity<IdentityRoleClaim<string>>()
            .HasData(
                new IdentityRoleClaim<string>
                {
                    Id = 60, RoleId = ApplicationRole.User.Id, ClaimType = Permission.ProjectRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 61, RoleId = ApplicationRole.User.Id, ClaimType = Permission.RecipeRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 62, RoleId = ApplicationRole.User.Id, ClaimType = Permission.RecipeWrite.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 63, RoleId = ApplicationRole.User.Id, ClaimType = Permission.TagRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 64, RoleId = ApplicationRole.User.Id, ClaimType = Permission.TagWrite.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 65, RoleId = ApplicationRole.User.Id, ClaimType = Permission.SampleRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 66, RoleId = ApplicationRole.User.Id, ClaimType = Permission.SampleWrite.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 67,
                    RoleId = ApplicationRole.User.Id,
                    ClaimType = Permission.ParameterRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 68,
                    RoleId = ApplicationRole.User.Id,
                    ClaimType = Permission.StepRead.ToString(),
                    ClaimValue = "true"
                },
                new IdentityRoleClaim<string>
                {
                    Id = 69,
                    RoleId = ApplicationRole.User.Id,
                    ClaimType = Permission.StepWrite.ToString(),
                    ClaimValue = "true"
                });
        #endregion
    }
}
