using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Infrastructure.Identity;

internal sealed class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender<ApplicationUser> emailSender,
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator,
    IOptionsMonitor<BearerTokenOptions> bearerTokenOptions,
    TimeProvider timeProvider) : IUserService
{
    public async Task RegisterAsync(string email, string password)
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
        var result = await userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            throw new FailedToRegisterUserException(string.Empty)
            {
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        
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
        string email,
        string password,
        string? twoFactorCode,
        string? twoFactorRecoveryCode,
        bool useCookies = false,
        bool useSessionCookies = false)
    {
        signInManager.AuthenticationScheme =
            useCookies ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var isPersistent = useCookies && !useSessionCookies;
        var result = await signInManager.PasswordSignInAsync(email, password, isPersistent, 
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

    public Task SignOutAsync()
    {
        return signInManager.SignOutAsync();
    }

    public async Task RefreshTokenAsync(string refreshToken)
    {
        var ticket = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector.Unprotect(refreshToken);
        var expiresUtc = ticket?.Properties.ExpiresUtc;
        var isExpired = expiresUtc is null || timeProvider.GetUtcNow() >= expiresUtc;

        if (isExpired) throw new RefreshTokenExpiredException(expiresUtc);
        
        var user = await signInManager.ValidateSecurityStampAsync(ticket?.Principal);
        var cp = await signInManager.CreateUserPrincipalAsync(user!);
        await httpContextAccessor.HttpContext!.SignInAsync(IdentityConstants.BearerScheme, cp);
    }

    public async Task ConfirmEmailAsync(string userId, string code, string? newEmail = default)
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
        catch (FormatException e)
        {
            throw; // TODO
        }

        if (string.IsNullOrWhiteSpace(newEmail))
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
}