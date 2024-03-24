using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Application.Identity.GetUserInfo;
using Terminal.Backend.Unit.Identity.Common;

namespace Terminal.Backend.Unit.Identity.GetUserInfo;

[TestSubject(typeof(GetUserInfoQueryHandler))]
public class GetUserInfoQueryHandlerTest
{
    private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
    private readonly UserManager<ApplicationUser> _userManagerMock = MocksFactory.CreateUserManager();

    private readonly GetUserInfoQueryHandler _handler;

    public GetUserInfoQueryHandlerTest()
    {
        _handler = new GetUserInfoQueryHandler(_httpContextAccessorMock, _userManagerMock);
    }

    [Fact]
    public async Task Handle_SuccessfulGetUserInfo_ReturnsUserInfo()
    {
        // Arange
        var id = Guid.NewGuid();
        const string email = "test@test.com";
        const bool isEmailConfirmed = true;
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>())
            .Returns(new ApplicationUser()
            {
                Id = id.ToString(),
                Email = email,
                EmailConfirmed = isEmailConfirmed
            });
        var query = new GetUserInfoQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Id.Should().Be(id.ToString());
        result.Email.Should().Be(email);
        result.IsEmailConfirmed.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_QueryFailureDueToMissingUser_ThrowsException()
    {
        // Arange
        _userManagerMock.GetUserAsync(Arg.Any<ClaimsPrincipal>()).ReturnsNull();
        var query = new GetUserInfoQuery();

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_QueryFailureDueToMissingHttpContext_ThrowsException()
    {
        // Arange
        _httpContextAccessorMock.HttpContext.ReturnsNull();
        var query = new GetUserInfoQuery();

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }
}
