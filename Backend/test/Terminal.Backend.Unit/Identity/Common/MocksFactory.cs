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

    public static HttpContext CreateHttpContext()
    {
        var context = Substitute.For<HttpContext>();
        var authenticationService = Substitute.For<IAuthenticationService>();
        var serviceProvider = Substitute.For<IServiceProvider>();
        var authSchemaProvider = Substitute.For<IAuthenticationSchemeProvider>();
        authSchemaProvider.GetDefaultAuthenticateSchemeAsync()!.Returns(
            Task.FromResult(new AuthenticationScheme(IdentityConstants.BearerScheme,
                IdentityConstants.BearerScheme,
                typeof(IAuthenticationHandler))));
        serviceProvider.GetService(typeof(IAuthenticationService)).Returns(authenticationService);
        serviceProvider.GetService(typeof(TimeProvider)).Returns(TimeProvider.System);
        serviceProvider.GetService(typeof(IAuthenticationSchemeProvider)).Returns(authSchemaProvider);
        context.RequestServices.Returns(serviceProvider);
        return context;
    }

    public static RoleManager<ApplicationRole> CreateRoleManager() =>
        Substitute.For<RoleManager<ApplicationRole>>(
            Substitute.For<IRoleStore<ApplicationRole>>(),
            Substitute.For<IEnumerable<IRoleValidator<ApplicationRole>>>(),
            Substitute.For<ILookupNormalizer>(),
            new IdentityErrorDescriber(),
            Substitute.For<ILogger<RoleManager<ApplicationRole>>>());
}
