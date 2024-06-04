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
        => (await this._projects
            .AsNoTracking()
            .Where(p => p.IsActive || p.IsActive == request.OnlyActive)
            .OrderBy(request.OrderingParameters)
            .Paginate(request.Parameters)
            .ToListAsync(ct)).AsGetProjectsDto();
}