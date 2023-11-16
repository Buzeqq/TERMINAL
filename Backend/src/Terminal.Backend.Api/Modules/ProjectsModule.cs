using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Project.ChangeStatus;
using Terminal.Backend.Application.Commands.Project.Create;
using Terminal.Backend.Application.Queries.Projects.Get;
using Terminal.Backend.Application.Queries.Projects.Search;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class ProjectsModule
{
    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/projects",async ([FromQuery] int pageSize, [FromQuery] int pageNumber, ISender sender, 
                    CancellationToken ct)
                => Results.Ok(await sender.Send(new GetProjectsQuery(pageNumber, pageSize), ct)));

        app.MapGet("api/projects/{id:guid}", async (
            Guid id,
            ISender sender, 
            CancellationToken ct) =>
        {
            var project = await sender.Send(new GetProjectQuery { ProjectId = id }, ct);
            return project is null ? Results.NotFound() : Results.Ok(project);
        });
 
        app.MapPost("api/projects", async (
            CreateProjectCommand command, 
            ISender sender, 
            CancellationToken ct) =>
        {
            command = command with { Id = ProjectId.Create() };
            await sender.Send(command, ct);
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
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeProjectStatusCommand(id, true);
            await sender.Send(command, ct);
            return Results.Ok();
        });
        
        app.MapPost("api/projects/{id:guid}/deactivate", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var command = new ChangeProjectStatusCommand(id, false);
            await sender.Send(command, ct);
            return Results.Ok();
        });

        app.MapGet("api/projects/search", async ([FromQuery] string searchPhrase, [FromQuery] int pageNumber, [FromQuery] int pageSize, ISender sender, CancellationToken ct) =>
        {
            var query = new SearchProjectQuery(searchPhrase, pageNumber, pageSize);
            var projects = await sender.Send(query, ct);
            return Results.Ok(projects);
        });
    }
}