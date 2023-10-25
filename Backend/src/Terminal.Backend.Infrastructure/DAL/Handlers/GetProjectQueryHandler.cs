using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, GetProjectDto?>
{
    private readonly DbSet<Project> _projects;

    public GetProjectQueryHandler(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }
    
    public async Task<GetProjectDto?> Handle(GetProjectQuery query, CancellationToken ct)
    {
        var projectId = query.ProjectId;
        var project = await _projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == projectId, ct);

        return project?.AsGetProjectDto();
    }
}