using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Samples.Create;
using Terminal.Backend.Application.Samples.Delete;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Application.Samples.Search;
using Terminal.Backend.Application.Samples.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Samples;

public static class SamplesModule
{
    private const string ApiRouteBase = "api/samples";

    private static void AddSamplesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
                CreateSampleCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                var id = SampleId.Create();
                command = command with { SampleId = id };
                await sender.Send(command, ct);
                return Results.Created(ApiRouteBase, new { id });
            }).RequireAuthorization(Permission.SampleWrite.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/example", () =>
            {
                var sample = new CreateSampleCommand(SampleId.Create(), ProjectId.Create(), null, new[]
                    {
                        new CreateSampleStepDto(new CreateSampleBaseParameterValueDto[]
                            {
                                new CreateSampleDecimalParameterValueDto(ParameterId.Create(), 0.111m),
                                new CreateSampleIntegerParameterValueDto(ParameterId.Create(), 2137),
                                new CreateSampleTextParameterValueDto(ParameterId.Create(), "text")
                            },
                            "comment")
                    },
                    new List<Guid>
                    {
                        TagId.Create(), TagId.Create(), TagId.Create()
                    },
                    "comment", false);

                return Results.Ok(sample);
            }).AllowAnonymous()
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/recent", async (
                [FromQuery] int length,
                ISender sender,
                CancellationToken ct) =>
            {
                if (length <= 0)
                {
                    return Results.BadRequest();
                }

                var recentSamples = await sender.Send(new GetRecentSamplesQuery(length), ct);
                return Results.Ok(recentSamples);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var query = new GetSampleQuery { Id = id };
                var sample = await sender.Send(query, ct);
                return sample is null ? Results.NotFound() : Results.Ok(sample);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/", async (
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize,
                [FromQuery] string? orderBy,
                [FromQuery] bool? desc,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetSamplesQuery(pageNumber, pageSize, orderBy ?? "CreatedAtUtc", desc ?? true);
                var samples = await sender.Send(query, ct);
                return Results.Ok(samples);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/amount", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetSamplesAmountQuery();
                var amount = await sender.Send(query, ct);
                return Results.Ok(amount);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapGet("/search", async (
                [FromQuery] string searchPhrase,
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new SearchSampleQuery(searchPhrase, pageNumber, pageSize);
                var samples = await sender.Send(query, ct);
                return Results.Ok(samples);
            }).RequireAuthorization(Permission.SampleRead.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                await sender.Send(new DeleteSampleCommand(id), ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.SampleDelete.ToString())
            .WithTags(SwaggerSetup.SampleTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateSampleCommand command,
                CancellationToken ct) =>
            {
                command = command with { Id = id };
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.SampleUpdate.ToString())
            .WithTags(SwaggerSetup.SampleTag);
    }

    public static void UseSamplesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiRouteBase).AddSamplesEndpoints();
    }
}