namespace Terminal.Backend.Application.Abstractions;

using Core.ValueObjects;

public interface IUserService
{
    Task RegisterAsync(Email email, Password password);

    Task SignInAsync(
        Email email,
        Password password,
        string? twoFactorCode,
        string? twoFactorRecoveryCode,
        bool useCookies = true,
        bool useSessionCookies = true);

    Task SignOutAsync();
    Task RefreshTokenAsync(string refreshToken);
    Task ConfirmEmailAsync(string userId, string code, Email? newEmail = default);
    Task ResendConfirmationEmailAsync(Email email);
    Task ForgotPasswordAsync(Email email);
    Task ResetPasswordAsync(Email email, Password newPassword, string code);
}
