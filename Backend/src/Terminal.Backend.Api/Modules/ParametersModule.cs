using MediatR;
using Terminal.Backend.Application.Commands.Parameter.ChangeStatus;
using Terminal.Backend.Application.Commands.Parameter.Define;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters.Get;
using Terminal.Backend.Core.ValueObjects;

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
            parameterDto = parameterDto with { Id = ParameterId.Create() };
            await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/decimal", async (
            CreateDecimalParameterDto parameterDto,
            ISender sender,
            CancellationToken ct) =>
        {
            parameterDto = parameterDto with { Id = ParameterId.Create() };
            await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameterDto.Name}", null);
        });
        
        app.MapPost("api/parameters/define/integer", async (
            CreateIntegerParameterDto parameter,
            ISender sender,
            CancellationToken ct) =>
        {
            parameter = parameter with { Id = ParameterId.Create() };
            await sender.Send(new DefineParameterCommand(parameter.AsParameter()), ct);
            return Results.Created($"api/parameters/{parameter.Name}", null);
        });

        app.MapGet("api/parameters/{id:guid}", async (
            Guid id, 
            ISender sender, 
            CancellationToken ct) =>
        {
            var parameter = await sender.Send(new GetParameterQuery { Id = id }, ct);
            return parameter is null ? Results.NotFound() : Results.Ok(parameter);
        });
        
        app.MapPost("api/parameters/{id:guid}/activate", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeParameterStatusCommand(id, true);
            await sender.Send(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/parameters/{id:guid}/deactivate", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeParameterStatusCommand(id, false);
            await sender.Send(command, ct);
            return Results.Ok();
        });
    }
}