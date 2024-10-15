using Terminal.Backend.Api.Parameters.Requests;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Parameters.ChangeStatus;
using Terminal.Backend.Application.Parameters.Define;
using Terminal.Backend.Application.Parameters.Get;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Parameters;

public static class ParametersModule
{
    private const string ApiBaseRoute = "parameters";

    private static IEndpointRouteBuilder MapParametersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var parameters = await sender.Send(new GetParametersQuery(), cancellationToken);

                return Results.Ok(parameters);
            }).RequireAuthorization(Permission.ParameterRead.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/text", async (
                DefineTextParameterRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = ParameterId.Create();

                await sender.Send(new DefineParameterCommand(
                    new TextParameter(
                        id,
                        request.Name,
                        request.ParentId,
                        request.AllowedValues.ToList(),
                        request.DefaultValue ?? 0)
                    ), cancellationToken);

                return Results.Created(ApiBaseRoute, new { id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/decimal", async (
                DefineDecimalParameterRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = ParameterId.Create();

                await sender.Send(
                    new DefineParameterCommand(new DecimalParameter(
                        id,
                        request.Name,
                        request.ParentId,
                        request.Unit,
                        request.Step,
                        request.DefaultValue ?? 0m)),
                    cancellationToken);

                return Results.Created(ApiBaseRoute, new { id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/define/integer", async (
                DefineIntegerParameterRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = ParameterId.Create();

                await sender.Send(new DefineParameterCommand(
                    new IntegerParameter(
                        id,
                        request.Name,
                        request.ParentId,
                        request.Unit,
                        request.Step,
                        request.DefaultValue ?? 0)),
                    cancellationToken);

                return Results.Created(ApiBaseRoute, new { id });
            }).RequireAuthorization(Permission.ParameterWrite.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var parameter = await sender.Send(new GetParameterQuery(id), cancellationToken);

                return parameter is null ? Results.NotFound() : Results.Ok(parameter);
            }).RequireAuthorization(Permission.ParameterRead.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeParameterStatusCommand(id, true);

                await sender.Send(command, cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ParameterUpdate.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeParameterStatusCommand(id, false);

                await sender.Send(command, cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ParameterUpdate.ToString())
            .WithTags(SwaggerSetup.ParameterTag);

        return app;
    }

    public static IEndpointRouteBuilder UseParametersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute)
            .MapParametersEndpoints();
        return app;
    }
}
