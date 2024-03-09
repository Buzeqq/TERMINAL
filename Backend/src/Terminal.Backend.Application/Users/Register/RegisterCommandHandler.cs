using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Users.Register;

internal sealed class RegisterCommandHandler(IUserService userService) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = request;
        await userService.RegisterAsync(email, password);
    }
}