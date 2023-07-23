using MediatR;
using Terminal.Backend.Application.Ping;

namespace Terminal.Backend.Api;

public static class PingApi
{
    public static WebApplication UsePingApi(this WebApplication app)
    {
        app.MapGet("api/ping", async (IRequestHandler<PingQuery, string> handler, CancellationToken ct) =>
        {
            var response = await handler.Handle(new PingQuery(), ct);
            return Results.Ok(response);
        });

        return app;
    }
}