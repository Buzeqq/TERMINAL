using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Identity.Register;

internal sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailConfirmationEmailSender emailConfirmationEmailSender) : IRequestHandler<RegisterCommand>
{
    // TODO: Add invitations handling
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = request;
        if (!new EmailAddressAttribute().IsValid(email))
        {
            throw new InvalidEmailException(email);
        }

        var user = await userManager.FindByEmailAsync(new Email(email));
        if (user is not null)
        {
            throw new EmailAlreadyExistsException(email);
        }

        var newUser = new ApplicationUser { Email = email, UserName = email };
        var result = await userManager.CreateAsync(newUser, password);

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
