using Microsoft.Extensions.DependencyInjection;
using Terminal.Backend.Infrastructure.DAL;
using Xunit;

namespace Terminal.Backend.Integration.Controllers;

/// <summary>
/// Class <c>BaseControllerTests</c> is used as arrangement part for testing endpoints.
/// </summary>
[Collection("api")]
public abstract class BaseControllerTests : IClassFixture<TerminalTestApp>, IDisposable
{
    protected HttpClient Client { get; }
    private readonly IServiceScope _scope;
    private readonly TerminalDbContext _terminalDbContext;
    private readonly UserDbContext _userDbContext;

    protected BaseControllerTests(TerminalTestApp terminalTestApp)
    {
        this.Client = terminalTestApp.Client;
        this._scope = terminalTestApp.Services.CreateScope();

        this._terminalDbContext = this._scope.ServiceProvider.GetRequiredService<TerminalDbContext>();
        this._userDbContext = this._scope.ServiceProvider.GetRequiredService<UserDbContext>();
    }

    public void Dispose()
    {
        this._terminalDbContext.Dispose();
        this._userDbContext.Dispose();
        this.Client.Dispose();
        this._scope.Dispose();
    }
}
