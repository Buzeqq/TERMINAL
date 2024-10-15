using System.Net;
using System.Net.Http.Json;
using Terminal.Backend.Api.Identity.Requests;

namespace Terminal.Backend.Integration.Identity;

public class LoginEndpointTests(TerminalTestAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task login_endpoint_should_return_200_ok_status_code_and_message_if_users_is_registered()
    {
        // Arrange
        var user = Users.Admin;
        var request = new LoginRequest(user.Email!, Users.Password, null, null);
        const bool useCookies = true;
        const bool useSessionCookies = true;

        // Act
        var response = await Client
            .PostAsJsonAsync($"identity/login?useCookies={useCookies}&useSessionCookies={useSessionCookies}",
                request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
