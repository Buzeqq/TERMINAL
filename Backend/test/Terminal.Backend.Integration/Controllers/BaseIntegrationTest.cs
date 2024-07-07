using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Integration.Controllers;

/// <summary>
/// Class <c>BaseIntegrationTest</c> is used as arrangement part for testing endpoints.
/// </summary>
[Collection("api")]
public abstract class BaseIntegrationTest : IClassFixture<TerminalTestAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly ISender Sender;

    /// <summary>
    /// Class <c>BaseIntegrationTest</c> is used as arrangement part for testing endpoints.
    /// </summary>
    protected BaseIntegrationTest(TerminalTestAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        Client = factory.CreateClient();
    }
}
