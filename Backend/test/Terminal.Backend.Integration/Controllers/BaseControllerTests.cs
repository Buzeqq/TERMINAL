using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

[Collection("api")]
public abstract class BaseControllerTests : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; }

    protected BaseControllerTests(OptionsProvider optionsProvider)
    {
        var app = new TerminalTestApp(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}