using Microsoft.AspNetCore.Identity;

namespace Terminal.Backend.Application.Common;

public class ApplicationUser : IdentityUser
{
    public ApplicationRole Role { get; set; } = ApplicationRole.DefaultRole;
}

public class ApplicationRole : IdentityRole
{
    public static readonly ApplicationRole Administrator = new("Administrator");
    public static readonly ApplicationRole Moderator = new("Moderator");
    public static readonly ApplicationRole User = new("User");
    public static readonly ApplicationRole Guest = new("Guest");

    private ApplicationRole(string name) : base(name)
    {
    }

    public static IEnumerable<ApplicationRole> AvailableRoles => new List<ApplicationRole>
    {
        Administrator,
        Moderator,
        User,
        Guest
    };

    public static readonly ApplicationRole DefaultRole = Guest;
}
