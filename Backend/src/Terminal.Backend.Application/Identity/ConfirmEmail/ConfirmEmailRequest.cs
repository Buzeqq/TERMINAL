namespace Terminal.Backend.Application.Identity.ConfirmEmail;

public class ConfirmEmailRequest
{
    public required string UserId { get; init; }
    public required string Code { get; init; }
    public string? ChangedEmail { get; init; }
}
