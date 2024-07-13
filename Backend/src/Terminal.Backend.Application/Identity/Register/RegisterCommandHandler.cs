using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Identity.Register;

internal sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IEmailConfirmationEmailSender emailConfirmationEmailSender) : IRequestHandler<RegisterCommand>
{
    // TODO: Add invitations handling
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (email, password, roleName) = request;

        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            throw new EmailAlreadyExistsException(email);
        }

        var newUser = new ApplicationUser { Email = email, UserName = email };
        var role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            throw new FailedToRegisterUserException("Role not found");
        }

        var result = await userManager.CreateAsync(newUser, password);
        if (!result.Succeeded)
        {
            throw new FailedToRegisterUserException(string.Empty)
            {
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        result = await userManager.AddToRoleAsync(newUser, roleName);
        if (!result.Succeeded)
        {
            throw new FailedToRegisterUserException(string.Empty)
            {
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        await emailConfirmationEmailSender.SendConfirmationEmailAsync(email, newUser);
    }
}
