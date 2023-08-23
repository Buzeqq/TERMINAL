using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly DbSet<Project> _projects;

    public ProjectRepository(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct)
        => await _projects.ToListAsync(ct);

    public Task<Project?> GetAsync(ProjectName name, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Project?> GetAsync(ProjectId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Project project, CancellationToken ct)
        => await _projects.AddAsync(project, ct);

    public Task UpdateAsync(Project project, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}