using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

/// <summary>
/// Class <c>BaseControllerTests</c> is used as arrangement part for testing endpoints.
/// </summary>
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