using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Projects.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectsQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetProjectsQuery, GetProjectsDto>
{
    private readonly DbSet<Project> _projects = dbContext.Projects;

    public async Task<GetProjectsDto> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var (searchPhrase, pagingParameters, orderingParameters) = request;

        var query = _projects
            .TagWith($"Get projects ordered [{orderingParameters}] and paginated [{pagingParameters}]")
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchPhrase))
        {
            query = query
                .Where(p => p.Name.Value.Contains(searchPhrase));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var projects = await query
            .Paginate(pagingParameters)
            .OrderBy(orderingParameters)
            .ToListAsync(cancellationToken);

        return GetProjectsDto.Create(projects, totalCount, pagingParameters);
    }
}
