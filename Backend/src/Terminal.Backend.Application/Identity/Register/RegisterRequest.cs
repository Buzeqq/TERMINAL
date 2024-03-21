using Mapster;

namespace Terminal.Backend.Application.Identity.Register;

public sealed record RegisterRequest(string Email, string Password);

public sealed class RegisterRequestRegister : IRegister
{
    public void Register(TypeAdapterConfig config) => config.NewConfig<RegisterRequest, RegisterCommand>();
}
