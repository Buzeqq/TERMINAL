using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Common.Emails;

internal interface IEmailConfirmationEmailSender
{
    Task SendConfirmationEmailAsync(Email email, ApplicationUser user, bool isChange = false);
}
