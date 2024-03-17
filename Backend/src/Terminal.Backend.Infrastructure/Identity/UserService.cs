using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;

namespace Terminal.Backend.Infrastructure.Identity;

internal sealed class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailSender<ApplicationUser> emailSender,
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator) : IUserService
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

    public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult>> SignInAsync(
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

        return TypedResults.Empty;
    }
}