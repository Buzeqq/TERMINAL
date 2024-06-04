using System.Net;
using FluentAssertions;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

public class PingControllerTests(OptionsProvider optionsProvider) : BaseControllerTests(optionsProvider)
{
    [Fact]
    public async Task health_check_endpoint_should_return_200_ok_status_code_and_message()
    {
        // Act
        var response = await this.Client.GetAsync("api/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
