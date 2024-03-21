using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Infrastructure.Identity;

namespace Terminal.Backend.Unit.Infrastructure.Identity;

public class UserServiceTests
{
    private readonly UserManager<ApplicationUser> _userManager = Substitute.For<UserManager<ApplicationUser>>(
        Substitute.For<IUserStore<ApplicationUser>>(),
        null, null, null, null, null, null, null, null);
    private readonly IEmailSender<ApplicationUser> _emailSender = Substitute.For<IEmailSender<ApplicationUser>>();
    private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
    private readonly LinkGenerator _linkGenerator = Substitute.For<LinkGenerator>();
    private readonly SignInManager<ApplicationUser> _signInManager = Substitute.For<SignInManager<ApplicationUser>>();
    private readonly IOptionsMonitor<BearerTokenOptions> _options = Substitute.For<IOptionsMonitor<BearerTokenOptions>>();
    private readonly TimeProvider _timeProvider = Substitute.For<TimeProvider>();
    
    private readonly UserService _userService;

    public UserServiceTests() =>
        this._userService = new UserService(this._userManager, this._signInManager, this._emailSender, this._httpContextAccessor, this._linkGenerator, this._options, this._timeProvider);

    [Fact]
    public async Task RegisterAsync_ValidUser_CreatesUser()
    {
        const string email = "test@example.com";
        const string password = "Test@1234";
        this._userManager.FindByEmailAsync(email)!
            .Returns(Task.FromResult<ApplicationUser>(null!));
        this._userManager.CreateAsync(Arg.Any<ApplicationUser>(), password)
            .Returns(Task.FromResult(IdentityResult.Success));
        this._userManager.GenerateEmailConfirmationTokenAsync(Arg.Any<ApplicationUser>())
            .Returns(Task.FromResult("Token"));
        this._httpContextAccessor.HttpContext.Returns(new DefaultHttpContext());

        await this._userService.RegisterAsync(email, password);

        await this._userManager.Received(1).CreateAsync(Arg.Any<ApplicationUser>(), password);
        await this._emailSender.Received(1).SendConfirmationLinkAsync(Arg.Any<ApplicationUser>(), email, 
Arg.Any<string>());
    }

    [Fact]
    public async Task RegisterAsync_InvalidEmail_ThrowsInvalidEmailException()
    {
        const string email = "invalid";
        const string password = "Test@1234";

        await Assert.ThrowsAsync<InvalidEmailException>(() => this._userService.RegisterAsync(email, password));
    }

    [Fact]
    public async Task RegisterAsync_EmailAlreadyExists_ThrowsEmailAlreadyExistsException()
    {
        const string email = "existing@example.com";
        const string password = "Test@1234";
        this._userManager.FindByEmailAsync(email)!.Returns(Task.FromResult(new ApplicationUser()));

        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => this._userService.RegisterAsync(email, password));
    }

    [Fact]
    public async Task RegisterAsync_FailureToCreateUser_ThrowsFailedToRegisterUserException()
    {
        const string email = "test@example.com";
        const string password = "Test@1234";
        this._userManager.FindByEmailAsync(email)!.Returns(Task.FromResult<ApplicationUser>(null!));
        this._userManager.CreateAsync(Arg.Any<ApplicationUser>(), password)
            .Returns(Task.FromResult(IdentityResult.Failed()));

        await Assert.ThrowsAsync<FailedToRegisterUserException>(() => this._userService.RegisterAsync(email, password));
    }
}