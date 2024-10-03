using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Projects.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class SearchProjectQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<SearchProjectQuery, GetProjectsDto>
{
    private readonly DbSet<Project> _projects = dbContext.Projects;

    public async Task<GetProjectsDto> Handle(SearchProjectQuery request, CancellationToken cancellationToken)
    {
        var projectsSearchQuery = _projects
            .AsNoTracking()
            .Where(p => EF.Functions.ILike(p.Name, $"%{request.SearchPhrase}%"))
            .Select(p => new GetProjectsDto.ProjectDto(p.Id, p.Name));

        var totalCount = await projectsSearchQuery.CountAsync(cancellationToken);

        var projects = await projectsSearchQuery
            .Paginate(request.Parameters)
            .ToListAsync(cancellationToken);

        return new GetProjectsDto(projects, totalCount, request.Parameters.PageNumber, request.Parameters.PageSize);
    }
}
