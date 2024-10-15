namespace Terminal.Backend.Api.Identity.Requests;

public record ConfirmEmailRequest(Guid UserId, string Code, string? ChangedEmail);
