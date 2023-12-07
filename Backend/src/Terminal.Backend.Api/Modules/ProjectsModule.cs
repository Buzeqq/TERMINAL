using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Commands.Project.ChangeStatus;
using Terminal.Backend.Application.Commands.Project.Create;
using Terminal.Backend.Application.Commands.Project.Delete;
using Terminal.Backend.Application.Commands.Project.Update;
using Terminal.Backend.Application.Queries.Projects.Get;
using Terminal.Backend.Application.Queries.Projects.Search;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class ProjectsModule
{
    private const string ApiRouteBase = "api/projects";

    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRouteBase, async (
                    [FromQuery] int pageSize,
                    [FromQuery] int pageNumber,
                    [FromQuery] bool? desc,
                    ISender sender,
                    CancellationToken ct
                ) =>
                Results.Ok(await sender.Send(new GetProjectsQuery(pageNumber, pageSize, desc ?? true), ct)))
            .RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet(ApiRouteBase + "/all", async (
                    [FromQuery] int pageSize,
                    [FromQuery] int pageNumber,
                    [FromQuery] bool? desc,
                    ISender sender,
                    CancellationToken ct
                ) =>
                Results.Ok(await sender.Send(new GetProjectsQuery(pageNumber, pageSize, desc ?? true, false), ct)))
            .RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet(ApiRouteBase + "/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var project = await sender.Send(new GetProjectQuery { ProjectId = id }, ct);
                return project is null ? Results.NotFound() : Results.Ok(project);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost(ApiRouteBase, async (
                CreateProjectCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = ProjectId.Create() };
                await sender.Send(command, ct);
                return Results.Created(ApiRouteBase, new { command.Id });
            }).RequireAuthorization(Permission.ProjectWrite.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost(ApiRouteBase + "/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeProjectStatusCommand(id, true);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost(ApiRouteBase + "/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ChangeProjectStatusCommand(id, false);
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet(ApiRouteBase + "/search", async ([FromQuery] string searchPhrase, [FromQuery] int pageNumber,
                [FromQuery] int pageSize, ISender sender, CancellationToken ct) =>
            {
                var query = new SearchProjectQuery(searchPhrase, pageNumber, pageSize);
                var projects = await sender.Send(query, ct);
                return Results.Ok(projects);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet(ApiRouteBase + "/amount", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetProjectsAmountQuery();
                var amount = await sender.Send(query, ct);
                return Results.Ok(amount);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet(ApiRouteBase + "/amount/all", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetProjectsAmountQuery(false);
                var amount = await sender.Send(query, ct);
                return Results.Ok(amount);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapDelete(ApiRouteBase + "/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                await sender.Send(new DeleteProjectCommand(id), ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectDelete.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPatch(ApiRouteBase + "/{id:guid}", async (
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
}