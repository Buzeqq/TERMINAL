using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.UpdateAccount;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.UpdateAccount;

[TestSubject(typeof(UpdateAccountCommandHandler))]
public class UpdateAccountCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();
    private readonly IEmailConfirmationEmailSender _emailConfirmationEmailSenderMock =
        Substitute.For<IEmailConfirmationEmailSender>();
    private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

    private readonly UpdateAccountCommandHandler _handler;

    public UpdateAccountCommandHandlerTest()
    {
        _handler = new UpdateAccountCommandHandler(_userManagerMock, _emailConfirmationEmailSenderMock, _httpContextAccessorMock);
    }

    [Fact]
    public async Task Handle_SuccessfulPasswordUpdate_ReturnsVoid()
    {
        // Arrange
        var command = new UpdateAccountCommand(null, "strong-password1", "WeakPassword1");
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(user);
        _userManagerMock.ChangePasswordAsync(user, command.OldPassword!, command.NewPassword!)
            .Returns(IdentityResult.Success);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManagerMock
            .Received(1)
            .ChangePasswordAsync(user, command.OldPassword!, command.NewPassword!);
    }

    [Fact]
    public async Task Handle_FailurePasswordUpdateDueToMissingUser_ThrowsException()
    {
        // Arrange
        var command = new UpdateAccountCommand(null, "strong-password1", "WeakPassword1");
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).ReturnsNull();

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailurePasswordUpdateDueToMissingOldPassword_ThrowsException()
    {
        // Arrange
        var command = new UpdateAccountCommand(null, "strong-password1", null);
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(user);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidPasswordException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailurePasswordUpdateDueToUpdateInternalError_ThrowsException()
    {
        // Arrange
        var command = new UpdateAccountCommand(null, "strong-password1", "WeakPassword1");
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(user);
        _userManagerMock.ChangePasswordAsync(user, command.OldPassword!, command.NewPassword!)
            .Returns(IdentityResult.Failed([new IdentityError { Code = "000", Description = "Database error" }]));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<FailedToUpdatePasswordException>(() =>
            _handler.Handle(command, CancellationToken.None));
        exception.Errors.Should().ContainSingle(e => e == "Database error");
    }

    [Fact]
    public async Task Handle_SuccessfulNewEmailConfirmationSend_ReturnsVoid()
    {
        // Arrange
        var command = new UpdateAccountCommand("newValidEmail@test.com", null, null);
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(user);
        _userManagerMock.GetEmailAsync(user).Returns("oldEmail@test.com");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _emailConfirmationEmailSenderMock
            .Received(1)
            .SendConfirmationEmailAsync(command.NewEmail!, user, true);
    }

    [Fact]
    public async Task Handle_SilentFailureEmailUpdateDueToNewEmailSameAsOldEmail_ReturnsVoid()
    {
        // Arrange
        var command = new UpdateAccountCommand("old-email@test.com", null, null);
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).Returns(user);
        _userManagerMock.GetEmailAsync(user).Returns(command.NewEmail!);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _emailConfirmationEmailSenderMock
            .Received(0)
            .SendConfirmationEmailAsync(command.NewEmail!, user, true);
    }
}
