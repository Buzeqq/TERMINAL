using Terminal.Backend.Application.Common;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Unit.Identity.Common;

public static class UserFactory
{
    public static ApplicationUser Create(UserId id, string email = "test@test.com") => new()
    {
        Id = id,
        Email = email
    };
}
