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

    public async Task<GetProjectsDto> Handle(GetProjectsQuery request,
        CancellationToken ct)
    {
        var shouldSearch = request.SearchPhrase is not null && !string.IsNullOrWhiteSpace(request.SearchPhrase);

        var query = _projects
            .AsNoTracking()
            .Where(p => p.IsActive || p.IsActive == request.OnlyActive);

        if (shouldSearch)
        {
            query = query
                .Where(p => EF.Functions.ILike(p.Name, $"%{request.SearchPhrase}%"));
        }

        var totalCount = await query.CountAsync(ct);
        return (await query
            .Paginate(request.Parameters)
            .OrderBy(request.OrderingParameters)
            .ToListAsync(ct)).AsGetProjectsDto(totalCount);
    }
}
