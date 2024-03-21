namespace Terminal.Backend.Application.Abstractions;

public interface IUserService
{
    Task RegisterAsync(string email, string password);

    Task SignInAsync(
        string email,
        string password,
        string? twoFactorCode,
        string? twoFactorRecoveryCode,
        bool useCookies = true,
        bool useSessionCookies = true);

    Task SignOutAsync();
    Task RefreshTokenAsync(string refreshToken);
    Task ConfirmEmailAsync(string userId, string code, string? newEmail = default);
}