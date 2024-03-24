﻿using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.ConfirmEmail;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.ConfirmEmail;

[TestSubject(typeof(ConfirmEmailCommandHandler))]
public class ConfirmEmailCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();

    private readonly ConfirmEmailCommandHandler _handler;

    public ConfirmEmailCommandHandlerTest()
    {
        _handler = new ConfirmEmailCommandHandler(_userManagerMock);
    }

    [Fact]
    public async Task Handle_SuccessfulEmailConfirmForNewEmail_ReturnsVoid()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(user);
        var command = new ConfirmEmailCommand("id", "code", null);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManagerMock.Received(1)
            .ConfirmEmailAsync(user, EncodeCode(command.Code));
    }

    [Fact]
    public async Task Handle_SuccessfulEmailConfirmForEmailChange_ReturnsVoid()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(user);
        var command = new ConfirmEmailCommand("id", "code", "test2@test.com");
        _userManagerMock.ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code))
            .Returns(IdentityResult.Success);
        _userManagerMock.SetUserNameAsync(user, command.NewEmail!)
            .Returns(IdentityResult.Success);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManagerMock.Received(1)
            .ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code));
        await _userManagerMock.Received(1)
            .SetUserNameAsync(user, command.NewEmail!);
    }

    [Fact]
    public async Task Handle_FailureEmailConfirmForEmailChangeDueToChangeEmailFailure_ThrowsException()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(user);
        var command = new ConfirmEmailCommand("id", "code", "test2@test.com");
        _userManagerMock.ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code))
            .Returns(IdentityResult.Failed());

        // Act & Assert
        await Assert.ThrowsAsync<FailedToUpdateEmailException>(() => _handler.Handle(command, CancellationToken.None));
        await _userManagerMock.Received(1)
            .ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code));
        await _userManagerMock.Received(0)
            .SetUserNameAsync(user, command.NewEmail!);
    }

    [Fact]
    public async Task Handle_FailureEmailConfirmForEmailChangeDueToSetUserNameFailure_ThrowsException()
    {
        // Arrange
        var user = UserFactory.Create("id", "test@test.com");
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(user);
        var command = new ConfirmEmailCommand("id", "code", "test2@test.com");
        _userManagerMock.ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code))
            .Returns(IdentityResult.Success);
        const string errorDescription = "desc";
        _userManagerMock.SetUserNameAsync(user, command.NewEmail!)
            .Returns(IdentityResult.Failed([new IdentityError { Code = "code", Description = errorDescription }]));

        // Act & Assert
        var e = await Assert.ThrowsAsync<FailedToUpdateEmailException>(() => _handler.Handle(command, CancellationToken.None));
        e.Errors.Should().ContainSingle(d => d == errorDescription);
        await _userManagerMock.Received(1)
            .ChangeEmailAsync(user, command.NewEmail!, EncodeCode(command.Code));
        await _userManagerMock.Received(1)
            .SetUserNameAsync(user, command.NewEmail!);
    }

    [Fact]
    public async Task Handle_FailureEmailConfirmDueToMissingUser_ThrowsException()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).ReturnsNull();
        var command = new ConfirmEmailCommand("id", "code", "test2@test.com");

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailureEmailConfirmDueToBadCode_ThrowsException()
    {
        // Arrange
        var command = new ConfirmEmailCommand("id", "   ", "test2@test.com");

        // Act & Assert
        await Assert.ThrowsAsync<BadCodeException>(() => _handler.Handle(command, CancellationToken.None));
    }

    private static string EncodeCode(string code) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
}
