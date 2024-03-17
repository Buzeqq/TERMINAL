using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.Register;

internal sealed class RegisterCommandHandler(IUserService userService) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = request;
        
        // TODO: Add invitations handling
        
        await userService.RegisterAsync(email, password);
    }
}