using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Common.Emails;

internal sealed class EmailConfirmationEmailSender(
    UserManager<ApplicationUser> userManager,
    LinkGenerator linkGenerator,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender<ApplicationUser> emailSender) : IEmailConfirmationEmailSender
{
    public async Task SendConfirmationEmailAsync(Email email, ApplicationUser newUser)
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
}
