using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Commands;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class ProjectsModule
{
    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/projects",
            async (IQueryHandler<GetProjectsQuery, IEnumerable<GetProjectsDto>> handler, 
                    CancellationToken ct)
                => Results.Ok(await handler.HandleAsync(new GetProjectsQuery(), ct)));

        app.MapPost("api/projects", async (
            CreateProjectCommand command,
            ICommandHandler<CreateProjectCommand> handler, 
            CancellationToken ct) =>
        {
            command = command with { Id = ProjectId.Create() };
            await handler.HandleAsync(command, ct);
            return Results.Created("api/projects", new { command.Id });
        });
    }
}