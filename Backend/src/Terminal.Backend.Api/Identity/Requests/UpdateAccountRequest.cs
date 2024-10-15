namespace Terminal.Backend.Api.Identity.Requests;

public record UpdateAccountRequest(string? NewEmail, string? NewPassword, string? OldPassword);
