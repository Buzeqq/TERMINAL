using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Infrastructure.Identity.Mails;

internal sealed class EmailSender(
    ILogger<EmailSender> logger,
    IHttpClientFactory clientFactory,
    IOptions<EmailSenderOptions> options) : IEmailSender<ApplicationUser>
{
    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var client = clientFactory.CreateClient(nameof(EmailSender));
        logger.LogInformation("Sending confirmation link to {@Email}", email);

        var requestBody = new SendEmailRequest(
            new SendEmailRequest.Sender(options.Value.From, "Terminal Client"),
            [new SendEmailRequest.Recipient(email, user.UserName ?? string.Empty)],
            "Confirm your email",
            $"Please confirm your account by <a href=\"{confirmationLink}\">clicking here</a>");
        var content = JsonContent.Create(requestBody);
        var response = await client.PostAsync("/v1/email", content);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Successfully sent confirmation link to {@Email}", email);
            return;
        }

        logger.LogError("Failed to send confirmation link to {@Email}.", email);
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) => Task.CompletedTask;

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var client = clientFactory.CreateClient(nameof(EmailSender));
        logger.LogInformation("Sending password reset code to {@Email}", email);

        var requestBody = new SendEmailRequest(
            new SendEmailRequest.Sender(options.Value.From, "Terminal Client"),
            [new SendEmailRequest.Recipient(email, user.UserName ?? string.Empty)],
            "Password reset code",
            $"Here is your password reset code: <strong>{resetCode}</strong>");
        var content = JsonContent.Create(requestBody);
        var response = await client.PostAsync("/v1/email", content);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Successfully sent password reset code to {@Email}", email);
            return;
        }

        logger.LogError("Failed to send password code to {@Email}.", email);
    }
}
