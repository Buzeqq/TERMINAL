using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.Logout;

internal sealed class LogoutCommandHandler(IUserService userService) : IRequestHandler<LogoutCommand>
{
    public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return userService.SignOutAsync();
    }
}