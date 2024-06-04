using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Application.Identity.Logout;

internal sealed class LogoutCommandHandler(SignInManager<ApplicationUser> signInManager) : IRequestHandler<LogoutCommand>
{
    public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        => signInManager.SignOutAsync();
}
