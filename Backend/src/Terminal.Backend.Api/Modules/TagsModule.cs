using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Tag.ChangeStatus;
using Terminal.Backend.Application.Commands.Tag.Create;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Api.Modules;

public static class TagsModule
{
    public static void UseTagEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/tags", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            ISender sender,
            CancellationToken ct
            ) => await sender.Send(new GetTagsQuery(pageNumber, pageSize), ct));
        
        app.MapPost("api/tags", async (
                CreateTagCommand command, 
                ISender sender, 
                CancellationToken ct) =>
            {
                await sender.Send(command, ct);
                return Results.Created("api/tags", null);
            });

        // app.MapPatch("api/tags/{name}", async (
        //     string name, 
        //     ChangeTagStatusCommand command,
        //     ICommandHandler<ChangeTagStatusCommand> handler,
        //     CancellationToken ct) =>
        // {
        //     command = command with { Name = name };
        //     await handler.HandleAsync(command, ct);
        //     return Results.Ok();
        // });
        
        app.MapPost("api/tags/{id:guid}/activate", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(id, true);
            await sender.Send(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/tags/{id:guid}/deactivate", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(id, false);
            await sender.Send(command, ct);
            return Results.Ok();
        });

        app.MapGet("api/tags/search", async (
            [FromQuery] string searchPhrase,
            ISender sender,
            CancellationToken ct
            ) =>
        {
            var query = new SearchTagQuery(searchPhrase);
            var tags = await sender.Send(query, ct);

            return Results.Ok(tags);
        });

        app.MapGet("api/tags/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var tag = await sender.Send(new GetTagQuery { TagId = id }, ct);
            return tag is null ? Results.NotFound() : Results.Ok(tag);
        });
    }
}