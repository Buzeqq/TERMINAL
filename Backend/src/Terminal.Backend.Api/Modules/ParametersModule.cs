using MediatR;
using Terminal.Backend.Application.Commands.Parameter.ChangeStatus;
using Terminal.Backend.Application.Commands.Parameter.Define;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Api.Modules;

public static class ParametersModule
{
    public static void UseParametersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/parameters", async (ISender sender, CancellationToken ct) =>
        {
            var parameters = await sender.Send(new GetParametersQuery(), ct);
            return Results.Ok(parameters);
        });
        
        app.MapPost("api/parameters/define/text", async (
            CreateTextParameterDto parameterDto, 
            ISender sender, 
            CancellationToken ct) =>
        {
            await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/decimal", async (
            CreateDecimalParameterDto parameterDto,
            ISender sender,
            CancellationToken ct) =>
        {
            await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/integer", async (
            CreateIntegerParameterDto parameter,
            ISender sender,
            CancellationToken ct) =>
        {
            await sender.Send(new DefineParameterCommand(parameter.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameter.Name}", null);
        });

        app.MapGet("api/parameters/{name}", async (
            string name, 
            ISender sender, 
            CancellationToken ct) =>
        {
            var parameter = await sender.Send(new GetParameterQuery { Name = name }, ct);
            return parameter is null ? Results.NotFound() : Results.Ok(parameter);
        });
        
        app.MapPost("api/parameters/{name}/activate", async (
            string name,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeParameterStatusCommand(name, true);
            await sender.Send(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/parameters/{name}/deactivate", async (
            string name,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeParameterStatusCommand(name, false);
            await sender.Send(command, ct);
            return Results.Ok();
        });
    }
}