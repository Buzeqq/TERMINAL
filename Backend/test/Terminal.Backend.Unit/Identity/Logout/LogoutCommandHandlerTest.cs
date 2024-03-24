using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Identity.Login;
using Terminal.Backend.Application.Identity.Logout;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.Logout;

[TestSubject(typeof(LoginCommandHandler))]
public class LogoutCommandHandlerTest
{
    private readonly LogoutCommandHandler _handler;
    private readonly SignInManager<ApplicationUser> _mockSignInManager;

    public LogoutCommandHandlerTest()
    {
        _mockSignInManager = MocksFactory.CreateSignInManager();
        _handler = new LogoutCommandHandler(_mockSignInManager);
    }

    [Fact]
    public async Task Handle_SuccessfulLogout_ReturnsVoid()
    {
        // Arrange
        var command = new LogoutCommand();
        // Act
        await _handler.Handle(command, CancellationToken.None);
        // Assert
        await _mockSignInManager.Received(1).SignOutAsync();
    }
}
