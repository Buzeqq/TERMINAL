using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.Login;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.Login;

[TestSubject(typeof(LoginCommandHandler))]
public class LoginCommandHandlerTest
{
    private readonly SignInManager<ApplicationUser> _mockSignInManager;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTest()
    {
        _mockSignInManager = MocksFactory.CreateSignInManager();
        var mockUserManager = MocksFactory.CreateUserManager();
        _handler = new LoginCommandHandler(_mockSignInManager, mockUserManager);
    }

    [Fact]
    public async Task Handle_SuccessfulLoginWithoutTwoFactor_ReturnsVoid()
    {
        // Arrange
        var loginCommand = new LoginCommand("test@example.com", "Password123", null, null, true, false);
        _mockSignInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.Success));

        // Act
        await _handler.Handle(loginCommand, CancellationToken.None);

        // Assert
        await _mockSignInManager.Received(1)
            .PasswordSignInAsync("test@example.com", "Password123", true, true);
    }

    [Fact]
    public async Task Handle_SuccessfulLoginWithTwoFactorCode_ReturnsVoid()
    {
        // Arrange
        var loginCommand = new LoginCommand("test@example.com", "Password123", "TwoFactorCode", null, false, true);
        _mockSignInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.TwoFactorRequired));
        _mockSignInManager.TwoFactorAuthenticatorSignInAsync(Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.Success));

        // Act
        await _handler.Handle(loginCommand, CancellationToken.None);

        // Assert
        await _mockSignInManager.Received(1).TwoFactorAuthenticatorSignInAsync("TwoFactorCode", true, true);
    }

    [Fact]
    public async Task Handle_SuccessfulLoginWithTwoFactorRecoveryCode_ReturnsVoid()
    {
        // Arrange
        var loginCommand = new LoginCommand("test@example.com", "Password123", null, "RecoveryCode", true, false);
        _mockSignInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.TwoFactorRequired));
        _mockSignInManager.TwoFactorRecoveryCodeSignInAsync(Arg.Any<string>())
            .Returns(Task.FromResult(SignInResult.Success));

        // Act
        await _handler.Handle(loginCommand, CancellationToken.None);

        // Assert
        await _mockSignInManager.Received(1).TwoFactorRecoveryCodeSignInAsync("RecoveryCode");
    }

    [Fact]
    public async Task Handle_LoginFailureDueToIncorrectCredentials_ThrowsException()
    {
        // Arrange
        var loginCommand = new LoginCommand("test@example.com", "wrongPassword", null, null, true, false);
        _mockSignInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.Failed));

        // Act & Assert
        await Assert.ThrowsAsync<LoginFailedException>(() => _handler.Handle(loginCommand, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_LoginFailureRequiresTwoFactorButNoCodeProvided_ThrowsException()
    {
        // Arrange
        var loginCommand = new LoginCommand("test@example.com", "Password123", null, null, true, true);
        _mockSignInManager.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
            .Returns(Task.FromResult(SignInResult.TwoFactorRequired));

        // Act & Assert
        await Assert.ThrowsAsync<LoginFailedException>(() => _handler.Handle(loginCommand, CancellationToken.None));
    }
}
