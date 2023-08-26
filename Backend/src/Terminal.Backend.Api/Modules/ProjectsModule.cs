using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class ProjectsModule
{
    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/projects",async (IQueryHandler<GetProjectsQuery, 
                    IEnumerable<GetProjectsDto>> handler, 
                    CancellationToken ct)
                => Results.Ok(await handler.HandleAsync(new GetProjectsQuery(), ct)));

        app.MapGet("api/projects/{id:guid}", async (
            Guid id,
            IQueryHandler<GetProjectQuery, Project?> handler, 
            CancellationToken ct) =>
        {
            var project = await handler.HandleAsync(new GetProjectQuery { ProjectId = id }, ct);
            return project is null ? Results.NotFound() : Results.Ok(project);
        });
 
        app.MapPost("api/projects", async (
            CreateProjectCommand command, 
            ICommandHandler<CreateProjectCommand> handler, 
            CancellationToken ct) =>
        {
            command = command with { Id = ProjectId.Create() };
            await handler.HandleAsync(command, ct);
            return Results.Created("api/projects", new { command.Id });
        });

        // app.MapPatch("api/projects/{id:guid}", async (
        //     Guid id,
        //     ChangeProjectStatusCommand command,
        //     ICommandHandler<ChangeProjectStatusCommand> handler,
        //     CancellationToken ct) =>
        // {
        //     command = command with { ProjectId = id };
        //     await handler.HandleAsync(command, ct);
        //     return Results.Ok();
        // });
        
        app.MapPost("api/projects/{id:guid}/activate", async (
            Guid id,
            ICommandHandler<ChangeProjectStatusCommand> handler,
            CancellationToken ct) =>
        {
            var command = new ChangeProjectStatusCommand(id, true);
            await handler.HandleAsync(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/projects/{id:guid}/deactivate", async (
            Guid id,
            ICommandHandler<ChangeProjectStatusCommand> handler,
            CancellationToken ct) =>
        {
            var command = new ChangeProjectStatusCommand(id, false);
            await handler.HandleAsync(command, ct);
            return Results.Ok();
        });
    }
}