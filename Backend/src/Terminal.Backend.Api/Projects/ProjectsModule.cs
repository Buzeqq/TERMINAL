using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Projects.Requests;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.Projects.ChangeStatus;
using Terminal.Backend.Application.Projects.Create;
using Terminal.Backend.Application.Projects.Delete;
using Terminal.Backend.Application.Projects.Get;
using Terminal.Backend.Application.Projects.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Projects;

public static class ProjectsModule
{
    private const string ApiRouteBase = "projects";

    private static IEndpointRouteBuilder AddProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] OrderDirection? orderDirection,
            [FromQuery] string? searchPhrase,
            ISender sender,
            CancellationToken cancellationToken
            ) => {
                var result = await sender.Send(
                    new GetProjectsQuery(
                        searchPhrase,
                        new PagingParameters(pageIndex, pageSize),
                        new OrderingParameters("Name", OrderDirection.Ascending)),
                    cancellationToken);

                return Results.Ok(result.Projects);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapGet("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var project = await sender.Send(new GetProjectQuery(id), cancellationToken);

                return project is null ? Results.NotFound() : Results.Ok(project);
            }).RequireAuthorization(Permission.ProjectRead.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/", async (
                CreateProjectRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = ProjectId.Create();

                await sender.Send(new CreateProjectCommand(id, request.Name), cancellationToken);

                return Results.Created(ApiRouteBase, new { id });
            }).RequireAuthorization(Permission.ProjectWrite.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/{id:guid}/activate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeProjectStatusCommand(id, true);

                await sender.Send(command, cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPost("/{id:guid}/deactivate", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new ChangeProjectStatusCommand(id, false);

                await sender.Send(command, cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteProjectCommand(id), cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectDelete.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                UpdateProjectRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var newName = request.Name is null ? null : new ProjectName(request.Name);

                await sender.Send(new UpdateProjectCommand(id, newName), cancellationToken);

                return Results.Ok();
            }).RequireAuthorization(Permission.ProjectUpdate.ToString())
            .WithTags(SwaggerSetup.ProjectTag);

        return app;
    }

    public static IEndpointRouteBuilder UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiRouteBase).AddProjectsEndpoints();
        return app;
    }
}
