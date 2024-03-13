using MediatR;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.DTO.Parameters;
using Terminal.Backend.Application.Parameters.ChangeStatus;
using Terminal.Backend.Application.Parameters.Define;
using Terminal.Backend.Application.Parameters.Get;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Parameters;

public static class ParametersModule
{
    private const string ApiBaseRoute = "api/parameters";

    private static void MapParametersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var parameters = await sender.Send(new GetParametersQuery(), ct);
                return Results.Ok(parameters);
            }).RequireAuthorization(Permission.ParameterRead.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/text", async (
                CreateTextParameterDto parameterDto,
                ISender sender,
                CancellationToken ct) =>
            {
                parameterDto = parameterDto with { Id = ParameterId.Create() };
                await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
                return Results.Created(ApiBaseRoute, new { parameterDto.Id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/decimal", async (
                CreateDecimalParameterDto parameterDto,
                ISender sender,
                CancellationToken ct) =>
            {
                parameterDto = parameterDto with { Id = ParameterId.Create() };
                await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
                return Results.Created(ApiBaseRoute, new { parameterDto.Id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/integer", async (
                CreateIntegerParameterDto parameterDto,
                ISender sender,
                CancellationToken ct) =>
            {
                parameterDto = parameterDto with { Id = ParameterId.Create() };
                await sender.Send(new DefineParameterCommand(parameterDto.AsParameter()), ct);
                return Results.Created(ApiBaseRoute, new { parameterDto.Id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var parameter = await sender.Send(new GetParameterQuery { Id = id }, ct);
                return parameter is null ? Results.NotFound() : Results.Ok(parameter);
            }).RequireAuthorization(Permission.ParameterRead.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeParameterStatusCommand(id, true);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ParameterUpdate.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeParameterStatusCommand(id, false);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ParameterUpdate.ToString())
            .WithTags(SwaggerSetup.ParameterTag);
    }

    public static void UseParametersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute)
            .MapParametersEndpoints();
    }
}