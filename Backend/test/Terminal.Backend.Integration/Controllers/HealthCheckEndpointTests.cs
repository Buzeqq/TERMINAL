using System.Net;
using FluentAssertions;

namespace Terminal.Backend.Integration.Controllers;

public class HealthCheckEndpointTests(TerminalTestAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task health_check_endpoint_should_return_200_ok_status_code_and_message()
    {
        // Act
        var response = await Client.GetAsync("api/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
