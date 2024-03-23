using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.ResendConfirmationEmail;

internal sealed class ResendConfirmationEmailCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailConfirmationEmailSender emailConfirmationEmailSender) : IRequestHandler<ResendConfirmationEmailCommand>
{
    public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        await emailConfirmationEmailSender.SendConfirmationEmailAsync(email, user);
    }
}
