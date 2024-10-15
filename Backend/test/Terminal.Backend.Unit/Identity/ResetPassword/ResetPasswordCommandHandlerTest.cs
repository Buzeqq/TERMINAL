using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.ResetPassword;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.ResetPassword;

[TestSubject(typeof(ResetPasswordCommandHandler))]
public class ResetPasswordCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManager = MocksFactory.CreateUserManager();

    private readonly ResetPasswordCommandHandler _handler;

    public ResetPasswordCommandHandlerTest()
    {
        _handler = new ResetPasswordCommandHandler(_userManager);
    }

    [Fact]
    public async Task Handle_SuccessfulResetPassword_ReturnsVoid()
    {
        // Arrange
        var command = new ResetPasswordCommand("test@test.com", "StrongPassword!23", "code");
        var user = UserFactory.Create(UserId.Create(), command.Email);
        _userManager.FindByEmailAsync(Arg.Is<string>(e => e == command.Email)).Returns(user);
        _userManager.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>()).Returns(true);
        _userManager.ResetPasswordAsync(
                Arg.Any<ApplicationUser>(),
                CodeEncoder.Decode(command.Code),
                command.NewPassword)
            .Returns(IdentityResult.Success);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManager.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
        await _userManager.Received(1).ResetPasswordAsync(
            Arg.Any<ApplicationUser>(),
            CodeEncoder.Decode(command.Code),
            command.NewPassword);
    }

    [Fact]
    public async Task Handle_FailureResetPasswordDueToMissingUser_ThrowsException()
    {
        // Arrange
        var command = new ResetPasswordCommand("test@test.com", "StrongPassword!23", "code");
        _userManager.FindByEmailAsync(command.Email).ReturnsNull();

        // Act && Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailureResetPasswordDueToEmailNotConfirmed_ThrowsException()
    {
        // Arrange
        var command = new ResetPasswordCommand("test@test.com", "StrongPassword!23", "code");
        var user = UserFactory.Create(UserId.Create(), command.Email);
        _userManager.FindByEmailAsync(Arg.Is<string>(e => e == command.Email)).Returns(user);
        _userManager.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>()).Returns(false);

        // Act && Assert
        var e = await Assert.ThrowsAsync<EmailNotConfirmedException>(() => _handler.Handle(command, CancellationToken.None));
        e.Details.Should().Be("To reset password you must confirm email first.");
    }

    [Fact]
    public async Task Handle_FailureResetPasswordDueToBadCode_ThrowsException()
    {
        // Arrange
        var command = new ResetPasswordCommand("test@test.com", "StrongPassword!23", " ");
        var user = UserFactory.Create(UserId.Create(), command.Email);
        _userManager.FindByEmailAsync(Arg.Is<string>(e => e == command.Email)).Returns(user);
        _userManager.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>()).Returns(true);

        // Act && Assert
        await Assert.ThrowsAsync<BadCodeException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailureResetPasswordDueToInternalError_ThrowsException()
    {
        // Arrange
        var command = new ResetPasswordCommand("test@test.com", "StrongPassword!23", "code");
        var user = UserFactory.Create(UserId.Create(), command.Email);
        _userManager.FindByEmailAsync(Arg.Is<string>(e => e == command.Email)).Returns(user);
        _userManager.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>()).Returns(true);
        const string description = "internal error";
        _userManager.ResetPasswordAsync(
                Arg.Any<ApplicationUser>(),
                CodeEncoder.Decode(command.Code),
                command.NewPassword)
            .Returns(IdentityResult.Failed([new IdentityError { Code = "code", Description = description }]));

        // Act & Assert
        var e = await Assert.ThrowsAsync<FailedToResetPasswordException>(() => _handler.Handle(command, CancellationToken.None));
        await _userManager.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
        await _userManager.Received(1).ResetPasswordAsync(
            Arg.Any<ApplicationUser>(),
            CodeEncoder.Decode(command.Code),
            command.NewPassword);
        e.Errors.Should().ContainSingle(description);
    }
}
