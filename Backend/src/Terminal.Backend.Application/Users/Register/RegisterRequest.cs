using Mapster;

namespace Terminal.Backend.Application.Users.Register;

public sealed record RegisterRequest(string Email, string Password) : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
    }
}