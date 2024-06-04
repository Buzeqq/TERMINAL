using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.ForgotPassword;

internal sealed class ForgotPasswordCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailSender<ApplicationUser> emailSender) : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            throw new EmailNotConfirmedException("To reset password you must confirm email first.");
        }

        var code = WebEncoders.Base64UrlEncode(
            Encoding.UTF8.GetBytes(await userManager.GeneratePasswordResetTokenAsync(user)));
        await emailSender.SendPasswordResetCodeAsync(user, email, HtmlEncoder.Default.Encode(code));
    }
}
