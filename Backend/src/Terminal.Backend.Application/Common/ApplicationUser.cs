using Microsoft.AspNetCore.Identity;

namespace Terminal.Backend.Application.Common;

public class ApplicationUser : IdentityUser;

public class ApplicationRole : IdentityRole
{
    public static readonly ApplicationRole Administrator = new("Administrator") { NormalizedName = "ADMINISTRATOR" };
    public static readonly ApplicationRole Moderator = new("Moderator") { NormalizedName = "MODERATOR" };
    public static readonly ApplicationRole User = new("User") { NormalizedName = "USER" };
    public static readonly ApplicationRole Guest = new("Guest") { NormalizedName = "GUEST" };

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
