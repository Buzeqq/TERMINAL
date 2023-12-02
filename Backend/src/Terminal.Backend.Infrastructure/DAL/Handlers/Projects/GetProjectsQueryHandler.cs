using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Queries.Projects.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, GetProjectsDto>
{
    private readonly DbSet<Project> _projects;

    public GetProjectsQueryHandler(TerminalDbContext dbContext) => _projects = dbContext.Projects;

    public async Task<GetProjectsDto> Handle(GetProjectsQuery request,
        CancellationToken ct)
        => (await _projects
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(request.OrderingParameters)
            .Paginate(request.Parameters)
            .ToListAsync(ct)).AsGetProjectsDto();
}