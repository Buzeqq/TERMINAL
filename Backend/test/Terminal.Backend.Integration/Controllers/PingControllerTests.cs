using System.Net;
using FluentAssertions;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

public class PingControllerTests(OptionsProvider optionsProvider) : BaseControllerTests(optionsProvider)
{
    [Fact]
    public async Task get_ping_endpoint_should_return_200_ok_status_code_and_message()
    {
        // Act
        var response = await Client.GetAsync("api/health");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}