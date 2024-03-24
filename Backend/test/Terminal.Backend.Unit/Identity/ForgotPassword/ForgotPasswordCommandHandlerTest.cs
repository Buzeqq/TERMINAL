using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.ForgotPassword;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.ForgotPassword;

[TestSubject(typeof(ForgotPasswordCommandHandler))]
public class ForgotPasswordCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();
    private readonly IEmailSender<ApplicationUser> _emailSenderMock = Substitute.For<IEmailSender<ApplicationUser>>();

    private readonly ForgotPasswordCommandHandler _handler;

    public ForgotPasswordCommandHandlerTest()
    {
        _handler = new ForgotPasswordCommandHandler(_userManagerMock, _emailSenderMock);
    }

    [Fact]
    public async Task Handle_SuccessfulForgotPasswordWhenEmailConfirmed_ReturnsVoid()
    {
        // Arrange
        const string code = "code";
        var user = UserFactory.Create("id", "test@test.com");
        var command = new ForgotPasswordCommand(user.Email!);
        _userManagerMock.FindByEmailAsync(command.Email).Returns(user);
        _userManagerMock.GeneratePasswordResetTokenAsync(user).Returns(code);
        _userManagerMock.IsEmailConfirmedAsync(user).Returns(true);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManagerMock.Received(1).IsEmailConfirmedAsync(user);
        await _userManagerMock.Received(1).GeneratePasswordResetTokenAsync(user);
        await _emailSenderMock.Received(1).SendPasswordResetCodeAsync(user, user.Email!, CodeEncoder.Encode(code));
    }

    [Fact]
    public async Task Handle_FailureForgotPasswordDueToEmailNotConfirmed_ThrowsException()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        var command = new ForgotPasswordCommand(user.Email!);
        _userManagerMock.FindByEmailAsync(command.Email).Returns(user);
        _userManagerMock.IsEmailConfirmedAsync(user).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<EmailNotConfirmedException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailureForgotPasswordDueToMissingUser_ThrowsException()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        var command = new ForgotPasswordCommand(user.Email!);
        _userManagerMock.FindByEmailAsync(command.Email).ReturnsNull();

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
