using MediatR;
using Terminal.Backend.Application.Commands.Tag.ChangeStatus;
using Terminal.Backend.Application.Commands.Tag.Create;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Api.Modules;

public static class TagsModule
{
    public static void UseTagEndpoints(this IEndpointRouteBuilder app)
    {
        // TODO: based on route/query parameter/command, fetch n most popular or all paginated 
        // app.MapGet("api/tags", async (
        //         int count, 
        //         IQueryHandler<GetMostPopularTagsQuery, GetTagsDto> handler, 
        //         CancellationToken ct) 
        //     => await handler.HandleAsync(new GetMostPopularTagsQuery { Count = count }, ct));
        app.MapGet("api/tags", async (
            ISender sender,
            CancellationToken ct
            ) => await sender.Send(new GetTagsQuery(), ct));
        
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
        
        app.MapPost("api/tags/{name}/activate", async (
            string name,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(name, true);
            await sender.Send(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/tags/{name}/deactivate", async (
            string name,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(name, false);
            await sender.Send(command, ct);
            return Results.Ok();
        });
    }
}