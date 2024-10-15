namespace Terminal.Backend.Api.Identity.Requests;

public record ResetPasswordRequest(string Email, string NewPassword, string Code);
