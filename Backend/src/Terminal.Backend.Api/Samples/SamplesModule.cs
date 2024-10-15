using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Common;
using Terminal.Backend.Api.Samples.Requests;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Samples.Create;
using Terminal.Backend.Application.Samples.Delete;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Application.Samples.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Samples;

public static class SamplesModule
{
    private const string ApiRouteBase = "samples";

    private static IEndpointRouteBuilder AddSamplesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
                CreateSampleRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = SampleId.Create();

                await sender.Send(new CreateSampleCommand(
                    id,
                    request.ProjectId,
                    request.RecipeId,
                    request.Steps.Select(s => new CreateSampleStepDto(
                        s.Values.SelectParameterValue<StepParameterValueDto>(
                            tm => new StepTextParameterValueDto(tm.ParameterId, tm.Value),
                            im => new StepIntegerParameterValueDto(im.ParameterId, im.Value),
                            dm => new StepDecimalParameterValueDto(dm.ParameterId, dm.Value)),
                        s.Comment)
                    ),
                    request.Tags.Select(t => new TagId(t)),
                    request.Comment,
                    request.SaveAsARecipe,
                    request.RecipeName is null ? null : new RecipeName(request.RecipeName)), cancellationToken);

                return Results.Created(ApiRouteBase, new { id });
            }).RequireAuthorization(Permission.SampleWrite.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/recent", async (
                [FromQuery] int length,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var recentSamples = await sender.Send(new GetRecentSamplesQuery(length), cancellationToken);

                return Results.Ok(recentSamples);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetSampleQuery(id);

                var sample = await sender.Send(query, cancellationToken);

                return sample is null ? Results.NotFound() : Results.Ok(sample);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/", async (
                [FromQuery] int pageIndex,
                [FromQuery] int pageSize,
                [FromQuery] string? orderBy,
                [FromQuery] OrderDirection? orderDirection,
                [FromQuery] string? searchPhrase,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetSamplesQuery(
                    searchPhrase,
                    new PagingParameters(pageIndex, pageSize),
                    new OrderingParameters(orderBy ?? "CreatedAtUtc", orderDirection));

                var samples = await sender.Send(query, cancellationToken);

                return Results.Ok(samples);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteSampleCommand(id), cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.SampleDelete.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateSampleRequest request,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new UpdateSampleCommand(
                    id,
                    request.ProjectId,
                    request.RecipeId,
                    request.Steps.Select(s => new UpdateSampleStepDto(
                        s.Id!.Value,
                        s.Values.SelectParameterValue<StepParameterValueDto>(
                            tm => new StepTextParameterValueDto(tm.ParameterId, tm.Value),
                            im => new StepIntegerParameterValueDto(im.ParameterId, im.Value),
                            dm => new StepDecimalParameterValueDto(dm.ParameterId, dm.Value)),
                        s.Comment)),
                    request.Tags.Select(t => new TagId(t)),
                    request.Comment), cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.SampleUpdate.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        return app;
    }

    public static IEndpointRouteBuilder UseSamplesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiRouteBase).AddSamplesEndpoints();
        return app;
    }
}
