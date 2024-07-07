using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.Login;

internal sealed class LoginCommandHandler(SignInManager<ApplicationUser> signInManager)
    : IRequestHandler<LoginCommand>
{
    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (email, password, twoFactorCode, twoFactorRecoveryCode, useCookies, useSessionCookies) = request;
        signInManager.AuthenticationScheme =
            useCookies ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var isPersistent = useCookies && !useSessionCookies;
        var result = await signInManager.PasswordSignInAsync(email, password, isPersistent, true);

        if (result.RequiresTwoFactor)
        {
            if (!string.IsNullOrWhiteSpace(twoFactorCode))
            {
                result = await signInManager.TwoFactorAuthenticatorSignInAsync(twoFactorCode, useSessionCookies, useSessionCookies);
            }
            else if (!string.IsNullOrWhiteSpace(twoFactorRecoveryCode))
            {
                result = await signInManager.TwoFactorRecoveryCodeSignInAsync(twoFactorRecoveryCode);
            }
        }

        if (!result.Succeeded)
        {
            throw new LoginFailedException(string.Empty);
        }
    }
}
