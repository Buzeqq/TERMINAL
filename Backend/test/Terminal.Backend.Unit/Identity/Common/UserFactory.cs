using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Unit.Identity.Common;

public static class UserFactory
{
    public static ApplicationUser Create(string id = "id", string email = "test@test.com") => new()
    {
        Id = id,
        Email = email
    };
}
