using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Tags.ChangeStatus;
using Terminal.Backend.Application.Tags.Create;
using Terminal.Backend.Application.Tags.Delete;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Application.Tags.Search;
using Terminal.Backend.Application.Tags.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Tags;

public static class TagsModule
{
    private const string ApiBaseRoute = "api/tags";

    private static void AddTagEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (
                    [FromQuery] int pageNumber,
                    [FromQuery] int pageSize,
                    [FromQuery] bool? desc,
                    ISender sender,
                    CancellationToken ct) =>
                sender.Send(new GetTagsQuery(pageNumber, pageSize, desc ?? true), ct))
            .RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapGet("/all", (
                    [FromQuery] int pageNumber,
                    [FromQuery] int pageSize,
                    [FromQuery] bool? desc,
                    ISender sender,
                    CancellationToken ct) =>
                sender.Send(new GetTagsQuery(pageNumber, pageSize, desc ?? true, false), ct))
            .RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/", async (
                CreateTagCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = TagId.Create() };
                await sender.Send(command, ct);
                return Results.Created(ApiBaseRoute, new { Id = command.Id.Value });
            }).RequireAuthorization(Permission.TagWrite.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeTagStatusCommand(id, true);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagUpdate.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeTagStatusCommand(id, false);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagUpdate.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapGet("/search", async (
                [FromQuery] string searchPhrase,
                ISender sender,
                CancellationToken ct
            ) =>
            {
                var query = new SearchTagQuery(searchPhrase);
                var tags = await sender.Send(query, ct);
                return Results.Ok(tags);
            }).RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var tag = await sender.Send(new GetTagQuery { TagId = id }, ct);
                return tag is null ? Results.NotFound() : Results.Ok(tag);
            }).RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapGet("/amount", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetTagsAmountQuery();
                var amount = await sender.Send(query, ct);
                return Results.Ok(amount);
            }).RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new DeleteTagCommand(id);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagDelete.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                [FromBody] UpdateTagCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = id };
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagDelete.ToString())
            .WithTags(SwaggerSetup.TagTag);
    }

    public static void UseTagEndpoints(this IEndpointRouteBuilder app) => app.MapGroup(ApiBaseRoute).AddTagEndpoints();
}