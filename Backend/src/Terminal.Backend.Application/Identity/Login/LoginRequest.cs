using Mapster;

namespace Terminal.Backend.Application.Identity.Login;

public sealed record LoginRequest(string Email, string Password, string? TwoFactorCode, string? TwoFactorRecoveryCode);

public sealed class LoginRequestRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(LoginRequest, bool, bool), LoginCommand>()
            .Map(dest => dest.UseCookies, src => src.Item2)
            .Map(dest => dest.UseSessionCookies, src => src.Item3);
    }
}