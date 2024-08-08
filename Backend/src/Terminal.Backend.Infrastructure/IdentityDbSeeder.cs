using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Infrastructure;

internal class IdentityDbSeeder(UserManager<ApplicationUser> userManager)
{
    public void Seed()
    {
        var userFaker = new Bogus.Faker<ApplicationUser>()
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UserName, (f, u) => u.Email)
            .RuleFor(u => u.EmailConfirmed, true);

        var users = userFaker.GenerateLazy(100);
        foreach (var user in users)
        {
            userManager.CreateAsync(user, "1qaz@WSX").Wait();
            userManager.AddToRoleAsync(user, nameof(ApplicationRole.User)).Wait();
        }
    }
}
