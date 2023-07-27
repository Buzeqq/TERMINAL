using MediatR;
using Terminal.Backend.Application.Ping;

namespace Terminal.Backend.Api.Ping;

public static class PingModule
{
    public static void UsePingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/ping", async (IRequestHandler<PingQuery, string> handler, CancellationToken ct) =>
        {
            var response = await handler.Handle(new PingQuery(), ct);
            return Results.Ok(response);
        });
    }
}