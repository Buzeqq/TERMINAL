using Mapster;

namespace Terminal.Backend.Application.Users.Login;

public sealed record LoginRequest(string Email, string Password, string? TwoFactorCode, string? TwoFactorRecoveryCode)
    : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginCommand>();
    }
}