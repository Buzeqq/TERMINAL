using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Infrastructure.Identity;

using System.Text.Encodings.Web;
using Core.ValueObjects;

internal sealed class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender<ApplicationUser> emailSender,
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator,
    IOptionsMonitor<BearerTokenOptions> bearerTokenOptions,
    TimeProvider timeProvider) : IUserService
{
    public async Task RegisterAsync(Email email, Password password)
    {
        if (!new EmailAddressAttribute().IsValid(email))
        {
            throw new InvalidEmailException(email);
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            throw new EmailAlreadyExistsException(email);
        }

        var newUser = new ApplicationUser { Email = email, UserName = email };
        var result = await userManager.CreateAsync(newUser, password.Value);

        if (!result.Succeeded)
        {
            throw new FailedToRegisterUserException(string.Empty)
            {
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        await this.SendConfirmationEmailAsync(email, newUser);
    }

    private async Task SendConfirmationEmailAsync(Email email, ApplicationUser newUser)
    {
        var code = WebEncoders.Base64UrlEncode(
            Encoding.UTF8.GetBytes(await userManager.GenerateEmailConfirmationTokenAsync(newUser)));
        var routeParameters = new RouteValueDictionary
        {
            ["userId"] = newUser.Id,
            ["code"] = code
        };

        var link = linkGenerator.GetUriByName(httpContextAccessor.HttpContext!, "Email confirmation endpoint", routeParameters)!;
        await emailSender.SendConfirmationLinkAsync(newUser, email, link);
    }

    public async Task SignInAsync(
        Email email,
        Password password,
        string? twoFactorCode,
        string? twoFactorRecoveryCode,
        bool useCookies = false,
        bool useSessionCookies = false)
    {
        signInManager.AuthenticationScheme =
            useCookies ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var isPersistent = useCookies && !useSessionCookies;
        var result = await signInManager.PasswordSignInAsync(email.Value, password.Value, isPersistent,
            true);

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
            throw new LoginFailedException(result.ToString());
        }
    }

    public Task SignOutAsync() => signInManager.SignOutAsync();

    public async Task RefreshTokenAsync(string refreshToken)
    {
        var ticket = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector.Unprotect(refreshToken);
        var expiresUtc = ticket?.Properties.ExpiresUtc;
        var isExpired = expiresUtc is null || timeProvider.GetUtcNow() >= expiresUtc;

        if (isExpired)
        {
            throw new RefreshTokenExpiredException(expiresUtc);
        }

        var user = await signInManager.ValidateSecurityStampAsync(ticket?.Principal);
        var cp = await signInManager.CreateUserPrincipalAsync(user!);
        await httpContextAccessor.HttpContext!.SignInAsync(IdentityConstants.BearerScheme, cp);
    }

    public async Task ConfirmEmailAsync(string userId, string code, Email? newEmail = default)
    {
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
                await userManager.SetUserNameAsync(user, newEmail);
            }
        }
    }

    public async Task ResendConfirmationEmailAsync(Email email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        await this.SendConfirmationEmailAsync(email, user);
    }

    public async Task ForgotPasswordAsync(Email email)
    {
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

    public async Task ResetPasswordAsync(Email email, Password newPassword, string code)
    {
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
