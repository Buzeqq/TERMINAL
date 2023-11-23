using MediatR;
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
        app.MapGet("api/ping", async (ISender sender, CancellationToken ct) =>
        {
            var response = await sender.Send(new PingQuery(), ct);
            return Results.Ok(response);
        }).AllowAnonymous();
    }
}