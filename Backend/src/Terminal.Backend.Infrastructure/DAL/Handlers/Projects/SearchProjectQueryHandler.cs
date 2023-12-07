using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Queries.Projects.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class SearchProjectQueryHandler : IRequestHandler<SearchProjectQuery, GetProjectsDto>
{
    private readonly DbSet<Project> _projects;

    public SearchProjectQueryHandler(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<GetProjectsDto> Handle(SearchProjectQuery request, CancellationToken cancellationToken)
        => new()
        {
            Projects = await _projects
                .AsNoTracking()
                .Where(p => EF.Functions.ILike(p.Name, $"%{request.SearchPhrase}%"))
                .Select(p => new GetProjectsDto.ProjectDto(p.Id, p.Name))
                .Paginate(request.Parameters)
                .ToListAsync(cancellationToken)
        };
}