namespace Terminal.Backend.Application.Identity.Login;

public sealed record LoginCommand(
    string Email,
    string Password,
    string? TwoFactorCode,
    string? TwoFactorRecoveryCode,
    bool UseCookies,
    bool UseSessionCookies)
    : IRequest;
