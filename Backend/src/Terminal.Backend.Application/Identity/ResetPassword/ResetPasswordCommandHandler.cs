using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.ResetPassword;

internal sealed class ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var (email, newPassword, code) = request;
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            throw new EmailNotConfirmedException("To reset password you must confirm email first.");
        }

        IdentityResult result;
        try
        {
            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            result = await userManager.ResetPasswordAsync(user, decodedCode, newPassword);
        }
        catch (FormatException)
        {
            throw new BadCodeException();
        }

        if (result.Succeeded)
        {
            return;
        }

        throw new FailedToResetPasswordException
        {
            Errors = result.Errors.Select(e => e.Description)
        };
    }
}
