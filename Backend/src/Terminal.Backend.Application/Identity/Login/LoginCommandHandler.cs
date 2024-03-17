using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.Login;

internal sealed class LoginCommandHandler(IUserService userService) : IRequestHandler<LoginCommand>
{
    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (email, password, twoFactorCode, recoveryCode, useCookies, useSessionCookies) = request;
        await userService.SignInAsync(email, password, twoFactorCode, recoveryCode, useCookies, useSessionCookies);
    }
}