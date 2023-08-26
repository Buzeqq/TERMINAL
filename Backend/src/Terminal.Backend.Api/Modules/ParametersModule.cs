namespace Terminal.Backend.Api.Modules;

public static class ParametersModule
{
    public static void UseParametersModule(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/parameters/text", async (CancellationToken ct) =>
        {

        });
        
        app.MapPost("api/parameters/decimal", async (CancellationToken ct) =>
        {

        });
        
        app.MapPost("api/parameters/integer", async (CancellationToken ct) =>
        {

        });
    }
}