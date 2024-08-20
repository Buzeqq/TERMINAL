using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Projects.ChangeStatus;
using Terminal.Backend.Application.Projects.Create;
using Terminal.Backend.Application.Projects.Delete;
using Terminal.Backend.Application.Projects.Get;
using Terminal.Backend.Application.Projects.Search;
using Terminal.Backend.Application.Projects.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Projects;

public static class ProjectsModule
{
    private const string ApiRouteBase = "api/projects";

    private static void AddProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
                    [FromQuery] int pageSize,
                    [FromQuery] int pageNumber,
                    [FromQuery] bool? desc,
                    [FromQuery] string? searchPhrase,
                    ISender sender,
                    CancellationToken ct
                ) =>
                Results.Ok(await sender.Send(new GetProjectsQuery(pageNumber, pageSize, desc ?? true, searchPhrase), ct)))
            .RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var project = await sender.Send(new GetProjectQuery { ProjectId = id }, ct);
                return project is null ? Results.NotFound() : Results.Ok(project);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/", async (
                CreateProjectCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = ProjectId.Create() };
                await sender.Send(command, ct);
                return Results.Created(ApiRouteBase, new { command.Id });
            }).RequireAuthorization(Permission.ProjectWrite.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeProjectStatusCommand(id, true);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeProjectStatusCommand(id, false);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                await sender.Send(new DeleteProjectCommand(id), ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectDelete.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                [FromBody] UpdateProjectCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = id };
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);
    }

    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app) => app.MapGroup(ApiRouteBase).AddProjectsEndpoints();
}
