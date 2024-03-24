using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.Refresh;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.Refresh;

[TestSubject(typeof(RefreshCommandHandler))]
public class RefreshCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();
    private readonly SignInManager<ApplicationUser> _signInManagerMock;
    private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
    private readonly IOptionsSnapshot<BearerTokenOptions> _optionsMock = Substitute.For<IOptionsSnapshot<BearerTokenOptions>>();
    private readonly TimeProvider _timeProviderMock = Substitute.For<TimeProvider>();

    private readonly RefreshCommandHandler _handler;

    public RefreshCommandHandlerTest()
    {
        _signInManagerMock = MocksFactory.CreateSignInManager(_userManagerMock);
        _handler = new RefreshCommandHandler(_signInManagerMock, _httpContextAccessorMock, _optionsMock, _timeProviderMock);
    }

    [Fact]
    public async Task Handle_SuccessfulRefresh_ReturnsVoid()
    {
        // Arrange
        const string refreshToken = "valid-token";
        var refreshCommand = new RefreshCommand(refreshToken);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(),
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            },
            IdentityConstants.BearerScheme);

        var bearerTokenOptions = new BearerTokenOptions();
        _optionsMock.Get(IdentityConstants.BearerScheme).Returns(bearerTokenOptions);
        bearerTokenOptions.RefreshTokenProtector = Substitute.For<ISecureDataFormat<AuthenticationTicket>>();
        bearerTokenOptions.RefreshTokenProtector.Unprotect(refreshToken).Returns(ticket);
        _timeProviderMock.GetUtcNow().Returns(DateTime.UtcNow);
        var user = new ApplicationUser();
        _signInManagerMock.ValidateSecurityStampAsync(Arg.Any<ClaimsPrincipal>())!
            .Returns(Task.FromResult(user));
        _signInManagerMock.CreateUserPrincipalAsync(user)
            .Returns(Task.FromResult(new ClaimsPrincipal()));
        var context = MocksFactory.CreateHttpContext();
        _httpContextAccessorMock.HttpContext.Returns(context);

        // Act
        await _handler.Handle(refreshCommand, CancellationToken.None);

        // Assert
        await _signInManagerMock.Received(1).ValidateSecurityStampAsync(Arg.Any<ClaimsPrincipal>());
        await _signInManagerMock.Received(1).CreateUserPrincipalAsync(user);
    }

    [Fact]
    public async Task Handle_RefreshFailureDueToRefreshTokenExpiration_ReturnsVoid()
    {
        // Arrange
        const string refreshToken = "expired-valid-token";
        var refreshCommand = new RefreshCommand(refreshToken);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(),
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            },
            IdentityConstants.BearerScheme);

        var bearerTokenOptions = new BearerTokenOptions();
        _optionsMock.Get(IdentityConstants.BearerScheme).Returns(bearerTokenOptions);
        bearerTokenOptions.RefreshTokenProtector = Substitute.For<ISecureDataFormat<AuthenticationTicket>>();
        bearerTokenOptions.RefreshTokenProtector.Unprotect(refreshToken).Returns(ticket);
        _timeProviderMock.GetUtcNow().Returns(DateTime.UtcNow.AddHours(2));
        var context = MocksFactory.CreateHttpContext();
        _httpContextAccessorMock.HttpContext.Returns(context);

        // Act & Assert
        await Assert.ThrowsAsync<RefreshTokenExpiredException>(() => _handler.Handle(refreshCommand, CancellationToken.None));
    }
}
