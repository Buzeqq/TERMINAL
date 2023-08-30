using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
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
            CreateDecimalParameterDto parameterDto,
            ICommandHandler<CreateParameterCommand> handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CreateParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/integer", async (
            CreateIntegerParameterDto parameter,
            ICommandHandler<CreateParameterCommand> handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CreateParameterCommand(parameter.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameter.Name}", null);
        });

        app.MapGet("api/parameters/{name}", async (
            string name, 
            IQueryHandler<GetParameterQuery, Parameter?> handler, 
            CancellationToken ct) =>
        {
            var parameter = await handler.HandleAsync(new GetParameterQuery { Name = name }, ct);
            return parameter is null ? Results.NotFound() : Results.Ok(parameter);
        });
    }
}