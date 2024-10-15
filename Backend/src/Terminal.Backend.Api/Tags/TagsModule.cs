using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Api.Tags.Requests;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.Tags.ChangeStatus;
using Terminal.Backend.Application.Tags.Create;
using Terminal.Backend.Application.Tags.Delete;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Application.Tags.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Tags;

public static class TagsModule
{
    private const string ApiBaseRoute = "tags";

    private static IEndpointRouteBuilder AddTagEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (
                    [FromQuery] int pageIndex,
                    [FromQuery] int pageSize,
                    OrderDirection? orderDirection,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                sender.Send(new GetTagsQuery(
                    new PagingParameters(pageIndex, pageSize),
                    new OrderingParameters("Name", orderDirection)), cancellationToken))
            .RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/", async (
                CreateTagRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = TagId.Create();
                await sender.Send(new CreateTagCommand(id, request.Name), cancellationToken);
                return Results.Created(ApiBaseRoute, new { id });
            }).RequireAuthorization(Permission.TagWrite.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeTagStatusCommand(id, true);
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagUpdate.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeTagStatusCommand(id, false);
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagUpdate.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var tag = await sender.Send(new GetTagQuery(id), cancellationToken);
                return tag is null ? Results.NotFound() : Results.Ok(tag);
            }).RequireAuthorization(Permission.TagRead.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteTagCommand(id);
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagDelete.ToString())
            .WithTags(SwaggerSetup.TagTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                UpdateTagRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new UpdateTagCommand(id, request.Name), cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.TagDelete.ToString())
            .WithTags(SwaggerSetup.TagTag);

        return app;
    }

    public static IEndpointRouteBuilder UseTagEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute).AddTagEndpoints();
        return app;
    }
}
