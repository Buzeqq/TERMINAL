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
    public async Task SendConfirmationEmailAsync(Email email, ApplicationUser user, bool isChange = false)
    {
        var code = isChange ? await userManager.GenerateEmailConfirmationTokenAsync(user) :
                await userManager.GenerateChangeEmailTokenAsync(user, email);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var routeParameters = new RouteValueDictionary
        {
            ["userId"] = user.Id,
            ["code"] = code
        };

        var link = linkGenerator.GetUriByName(httpContextAccessor.HttpContext!, "Email confirmation endpoint", routeParameters)!;
        await emailSender.SendConfirmationLinkAsync(user, email, link);
    }
}
