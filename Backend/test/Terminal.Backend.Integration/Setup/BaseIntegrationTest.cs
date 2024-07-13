using Microsoft.AspNetCore.Mvc.Testing;

namespace Terminal.Backend.Integration.Setup;

/// <summary>
/// Class <c>BaseIntegrationTest</c> is used as arrangement part for testing endpoints.
/// </summary>
[Collection("api")]
public abstract class BaseIntegrationTest : IClassFixture<TerminalTestAppFactory>
{
    protected readonly HttpClient Client;

    /// <summary>
    /// Class <c>BaseIntegrationTest</c> is used as arrangement part for testing endpoints.
    /// </summary>
    protected BaseIntegrationTest(TerminalTestAppFactory factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost/api/")
        });
    }
}
