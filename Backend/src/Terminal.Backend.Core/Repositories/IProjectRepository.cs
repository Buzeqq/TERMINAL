using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct);
    Task<Project?> GetAsync(ProjectName name, CancellationToken ct);
    Task<Project?> GetAsync(ProjectId id, CancellationToken ct);
    Task AddAsync(Project project, CancellationToken ct);
    Task UpdateAsync(Project project, CancellationToken ct);
}