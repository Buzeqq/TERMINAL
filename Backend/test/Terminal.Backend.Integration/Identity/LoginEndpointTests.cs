using System.Net;
using System.Net.Http.Json;
using Terminal.Backend.Application.Identity.Login;

namespace Terminal.Backend.Integration.Identity;

public class LoginEndpointTests(TerminalTestAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task login_endpoint_should_return_200_ok_status_code_and_message_if_users_is_registered()
    {
        // Arrange
        var user = Users.Admin;
        var request = new LoginRequest(user.Email!, Users.Password, null, null);

        // Act
        var response = await Client.PostAsJsonAsync($"identity/login?useCookies=true&useSessionCookies=true", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
