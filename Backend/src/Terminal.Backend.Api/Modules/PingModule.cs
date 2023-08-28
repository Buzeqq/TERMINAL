using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Api.Modules;

/// <summary>
/// Class <c>PingModule</c> is a class that defines endpoint for backend health check.
/// </summary>
public static class PingModule
{
    /// <summary>
    /// Extension method that defines health check endpoint.
    /// </summary>
    /// <param name="app">web app</param>
    public static void UsePingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/ping", async (IQueryHandler<PingQuery, string> handler, CancellationToken ct) =>
        {
            var response = await handler.HandleAsync(new PingQuery(), ct);
            return Results.Ok(response);
        });
    }
}