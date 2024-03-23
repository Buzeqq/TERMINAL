using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.Refresh;

internal sealed class RefreshCommandHandler(
    SignInManager<ApplicationUser> signInManager,
    IHttpContextAccessor httpContextAccessor,
    IOptionsSnapshot<BearerTokenOptions> options,
    TimeProvider timeProvider) : IRequestHandler<RefreshCommand>
{
    public async Task Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.RefreshToken;
        var ticket = options.Get(IdentityConstants.BearerScheme).RefreshTokenProtector.Unprotect(refreshToken);
        var expiresUtc = ticket?.Properties.ExpiresUtc;
        var isExpired = expiresUtc is null || timeProvider.GetUtcNow() >= expiresUtc;

        if (isExpired)
        {
            throw new RefreshTokenExpiredException(expiresUtc);
        }

        var user = await signInManager.ValidateSecurityStampAsync(ticket?.Principal);
        var cp = await signInManager.CreateUserPrincipalAsync(user!);
        await httpContextAccessor.HttpContext!.SignInAsync(IdentityConstants.BearerScheme, cp);
    }
}
