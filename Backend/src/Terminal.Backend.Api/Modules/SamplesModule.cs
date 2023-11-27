using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Measurement.Create;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Measurements.Get;
using Terminal.Backend.Application.Queries.Measurements.Search;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class SamplesModule
{
    public const string ApiRouteBase = "api/samples";
    public static void UseSamplesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRouteBase, async (
            CreateSampleCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var id = SampleId.Create();
            command = command with { SampleId = id };
            await sender.Send(command, ct);
            return Results.Created($"{ApiRouteBase}/{id}", null);
        }).RequireAuthorization(Permission.SampleWrite.ToString());

        app.MapGet(ApiRouteBase + "/example", () =>
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
            "comment");

            return Results.Ok(sample);
        });

        app.MapGet(ApiRouteBase + "/recent", async (
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
        }).RequireAuthorization(Permission.SampleRead.ToString());

        app.MapGet(ApiRouteBase + "/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
        {
            var query = new GetSampleQuery { Id = id };

            var sample = await sender.Send(query, ct);
            
            return sample is null ? Results.NotFound() : Results.Ok(sample);
        }).RequireAuthorization(Permission.SampleRead.ToString());

        app.MapGet(ApiRouteBase, async (
            [FromQuery] int pageNumber, 
            [FromQuery] int pageSize, 
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new GetSamplesQuery(pageNumber, pageSize);

            var samples = await sender.Send(query, ct);
            
            return Results.Ok(samples);
        }).RequireAuthorization(Permission.SampleRead.ToString());

        app.MapGet(ApiRouteBase + "/search", async (
            [FromQuery] string searchPhrase, 
            [FromQuery] int pageNumber, 
            [FromQuery] int pageSize, 
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new SearchSampleQuery(searchPhrase, pageNumber, pageSize);

            var samples = await sender.Send(query, ct);

            return Results.Ok(samples);
        }).RequireAuthorization(Permission.SampleRead.ToString());
    }
}