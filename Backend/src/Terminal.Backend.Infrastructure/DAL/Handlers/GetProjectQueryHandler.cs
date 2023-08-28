using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, Project?>
{
    private readonly DbSet<Project> _projects;

    public GetProjectQueryHandler(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }
    
    public async Task<Project?> HandleAsync(GetProjectQuery query, CancellationToken ct)
    {
        var projectId = query.ProjectId;
        var project = await _projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == projectId, ct);

        return project;
    }
}