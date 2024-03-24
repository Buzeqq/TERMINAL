using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Unit.Identity.Common;

public static class UserFactory
{
    public static ApplicationUser Create(string id, string email) => new ApplicationUser()
    {
        Id = id,
        Email = email
    };
}
