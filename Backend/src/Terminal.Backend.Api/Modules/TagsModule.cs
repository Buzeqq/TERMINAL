using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

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
            IQueryHandler<GetTagsQuery, IEnumerable<string>> handler,
            CancellationToken ct
            ) => await handler.HandleAsync(new GetTagsQuery(), ct));
        
        app.MapPost("api/tags", async (
                CreateTagCommand command, 
                ICommandHandler<CreateTagCommand> handler, 
                CancellationToken ct) =>
            {
                await handler.HandleAsync(command, ct);
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
            ICommandHandler<ChangeTagStatusCommand> handler,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(name, true);
            await handler.HandleAsync(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/tags/{name}/deactivate", async (
            string name,
            ICommandHandler<ChangeTagStatusCommand> handler,
            CancellationToken ct) =>
        {
            var command = new ChangeTagStatusCommand(name, false);
            await handler.HandleAsync(command, ct);
            return Results.Ok();
        });
    }
}