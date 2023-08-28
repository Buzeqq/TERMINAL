using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Api.Modules;

public static class ParametersModule
{
    public static void UseParametersModule(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/parameters/define/text", async (
            CreateTextParameterDto parameterDto, 
            ICommandHandler<CreateParameterCommand> handler, 
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CreateParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/decimal", async (
            DecimalParameter parameter,
            ICommandHandler<CreateParameterCommand> handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CreateParameterCommand(parameter), ct);
            return Results.Created($"api/parameters/{parameter.Name}", null);
        });
        
        app.MapPost("api/parameters/define/integer", async (
            IntegerParameter parameter,
            ICommandHandler<CreateParameterCommand> handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CreateParameterCommand(parameter), ct);
            return Results.Created($"api/parameters/{parameter.Name}", null);
        });
    }
}