using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Common;

namespace Terminal.Backend.Unit.Identity.Common;

public static class MocksFactory
{
    public static UserManager<ApplicationUser> CreateUserManager() => Substitute.For<UserManager<ApplicationUser>>(
        Substitute.For<IUserStore<ApplicationUser>>(),
        Substitute.For<IOptions<IdentityOptions>>(),
        Substitute.For<IPasswordHasher<ApplicationUser>>(),
        Substitute.For<IEnumerable<IUserValidator<ApplicationUser>>>(),
        Substitute.For<IEnumerable<IPasswordValidator<ApplicationUser>>>(),
        Substitute.For<ILookupNormalizer>(),
        Substitute.For<IdentityErrorDescriber>(),
        Substitute.For<IServiceProvider>(),
        Substitute.For<ILogger<UserManager<ApplicationUser>>>());

    public static SignInManager<ApplicationUser> CreateSignInManager(UserManager<ApplicationUser>? userManager = null) => Substitute.For<SignInManager<ApplicationUser>>(
        userManager ?? CreateUserManager(),
        Substitute.For<IHttpContextAccessor>(),
        Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>(),
        Substitute.For<IOptions<IdentityOptions>>(),
        Substitute.For<ILogger<SignInManager<ApplicationUser>>>(),
        Substitute.For<IAuthenticationSchemeProvider>(),
        Substitute.For<IUserConfirmation<ApplicationUser>>());

    public static IHttpContextAccessor CreateHttpContextAccessor(
        bool useCookies, string id, string userName, string emailAddress)
    {

        var accessor = Substitute.For<IHttpContextAccessor>();
        accessor.HttpContext?.User.Returns(new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.NameIdentifier, id), new Claim(ClaimTypes.Email, emailAddress)],
                useCookies ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme,
            ClaimTypes.Name,
            ClaimTypes.Role)));

        return accessor;
    }
}
