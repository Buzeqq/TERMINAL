using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Common.Emails;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.Register;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.Register;

[TestSubject(typeof(RegisterCommandHandler))]
public class RegisterCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();
    private readonly IEmailConfirmationEmailSender _emailConfirmationEmailSenderMock =
        Substitute.For<IEmailConfirmationEmailSender>();

    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTest()
    {
        _handler = new RegisterCommandHandler(_userManagerMock, _emailConfirmationEmailSenderMock);
    }

    [Fact]
    public async Task Handle_SuccessfulRegister_ReturnsVoid()
    {
        // Arrange
        var command = new RegisterCommand("newUser@test.com", "StrongPassword!1");
        _userManagerMock.FindByEmailAsync(command.Email).ReturnsNull();
        _userManagerMock.CreateAsync(Arg.Is<ApplicationUser>(u
            => u.Email == command.Email.Value && u.UserName == command.Email.Value), command.Password).Returns(IdentityResult.Success);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _userManagerMock
            .Received(1)
            .CreateAsync(Arg.Is<ApplicationUser>(u
                => u.Email == command.Email.Value && u.UserName == command.Email.Value), command.Password);
        await _emailConfirmationEmailSenderMock
            .Received(1)
            .SendConfirmationEmailAsync(command.Email, Arg.Is<ApplicationUser>(u
                => u.Email == command.Email.Value && u.UserName == command.Email.Value));
    }

    [Fact]
    public async Task Handle_FailureRegisterDueToTakenEmail_ThrowsException()
    {
        // Arrange
        var command = new RegisterCommand("newUser@test.com", "StrongPassword!1");
        _userManagerMock.FindByEmailAsync(command.Email).Returns(new ApplicationUser());

        // Act & Assert
        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_FailureRegisterDueToInternalError_ThrowsException()
    {
        // Arrange
        var command = new RegisterCommand("newUser@test.com", "StrongPassword!1");
        _userManagerMock.FindByEmailAsync(command.Email).ReturnsNull();
        _userManagerMock.CreateAsync(Arg.Is<ApplicationUser>(u
            => u.Email == command.Email.Value && u.UserName == command.Email.Value), command.Password)
            .Returns(IdentityResult.Failed([new IdentityError { Code = "0", Description = "Internal error" }]));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<FailedToRegisterUserException>(() =>
            _handler.Handle(command, CancellationToken.None));
        await _userManagerMock
            .Received(1)
            .CreateAsync(Arg.Is<ApplicationUser>(u
                => u.Email == command.Email.Value && u.UserName == command.Email.Value), command.Password);
        exception.Errors.Should().ContainSingle(e => e == "Internal error");
    }
}
