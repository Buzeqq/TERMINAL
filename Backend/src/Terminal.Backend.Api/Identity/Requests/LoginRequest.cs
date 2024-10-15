namespace Terminal.Backend.Api.Identity.Requests;

public record LoginRequest(string Email, string Password, string? TwoFactorCode, string? TwoFactorRecoveryCode);
