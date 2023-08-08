using FluentAssertions;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

public class PingControllerTests : BaseControllerTests
{
    [Fact]
    public async Task get_ping_endpoint_should_return_200_ok_status_code_and_message()
    {
        // Act
        var response = await Client.GetAsync("api/ping");
        var content = await response.Content.ReadAsStringAsync();
        content = content.Replace("\"", "");
        
        // Assert
        content.Should().Be("backend reachable");
    }

    public PingControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }
}