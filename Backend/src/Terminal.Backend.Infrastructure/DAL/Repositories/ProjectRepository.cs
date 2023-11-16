using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;

    public ProjectRepository(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public Task<Project?> GetAsync(ProjectId id, CancellationToken ct)
        => _projects.SingleOrDefaultAsync(p => p.Id == id, ct);

    public async Task AddAsync(Project project, CancellationToken ct)
        => await _projects.AddAsync(project, ct);

    public Task UpdateAsync(Project project, CancellationToken ct)
    {
        _projects.Update(project);
        return Task.CompletedTask;
    }
}