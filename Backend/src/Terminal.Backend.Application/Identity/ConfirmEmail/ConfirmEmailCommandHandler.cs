using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var (userId, code, newEmail) = request;
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {
            throw new BadCodeException();
        }

        if (newEmail is null)
        {
            await userManager.ConfirmEmailAsync(user, code);
        }
        else
        {
            var result = await userManager.ChangeEmailAsync(user, newEmail, code);
            if (result.Succeeded)
            {
                result = await userManager.SetUserNameAsync(user, newEmail);
            }

            if (!result.Succeeded)
            {
                throw new FailedToUpdateEmailException { Errors = result.Errors.Select(e => e.Description) };
            }
        }
    }
}
