using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.ResendConfirmationEmail;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.ResendConfirmationEmail;

[TestSubject(typeof(ResendConfirmationEmailCommandHandler))]
public class ResendConfirmationEmailCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();
    private readonly IEmailConfirmationEmailSender _emailSenderMock = Substitute.For<IEmailConfirmationEmailSender>();

    private readonly ResendConfirmationEmailCommandHandler _handler;

    public ResendConfirmationEmailCommandHandlerTest()
    {
        _handler = new ResendConfirmationEmailCommandHandler(_userManagerMock, _emailSenderMock);
    }

    [Fact]
    public async Task Handle_SuccessfulResendEmailConfirmForNewEmail_ReturnsVoid()
    {
        // Arrange
        var command = new ResendConfirmationEmailCommand("test@test.com");
        var user = UserFactory.Create(UserId.Create());
        _userManagerMock.FindByEmailAsync(command.Email).Returns(user);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _emailSenderMock.Received(1).SendConfirmationEmailAsync(command.Email, user);
    }

    [Fact]
    public async Task Handle_FailureResendEmailConfirmDueToMissingUser_ReturnsVoid()
    {
        // Arrange
        var command = new ResendConfirmationEmailCommand("test@test.com");
        _userManagerMock.FindByEmailAsync(command.Email).ReturnsNull();

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
