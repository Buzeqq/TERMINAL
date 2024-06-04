using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Application.Identity.UpdateAccount;

internal sealed class UpdateAccountCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailConfirmationEmailSender emailConfirmationEmailSender,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateAccountCommand>
{
    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var (newEmail, newPassword, oldPassword) = request;
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (newPassword is not null)
        {
            if (oldPassword is null)
            {
                throw new InvalidPasswordException();
            }

            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new FailedToUpdatePasswordException
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
        }

        if (newEmail is not null)
        {
            if ((await userManager.GetEmailAsync(user))! != newEmail)
            {
                await emailConfirmationEmailSender.SendConfirmationEmailAsync(newEmail, user, true);
            }
        }
    }
}
