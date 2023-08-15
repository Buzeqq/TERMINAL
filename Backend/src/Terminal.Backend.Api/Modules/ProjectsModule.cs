using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Api.Modules;

public static class ProjectsModule
{
    public static void UseProjectsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/projects",
            async (IRequestHandler<GetProjectsQuery, IEnumerable<GetProjectsDto>> handler, CancellationToken ct)
                => Results.Ok(await handler.Handle(new GetProjectsQuery(), ct)));
    }
}