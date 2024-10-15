namespace Terminal.Backend.Infrastructure.Identity.Mails;

internal sealed record SendEmailRequest(
    SendEmailRequest.Sender From,
    IEnumerable<SendEmailRequest.Recipient> To,
    string Subject,
    string Html)
{
    public record Sender(string Email, string Name);
    public record Recipient(string Email, string Name);
}
