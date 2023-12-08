using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetAsync(ProjectId id, CancellationToken ct);
    Task AddAsync(Project project, CancellationToken ct);
    Task UpdateAsync(Project project, CancellationToken ct);
    Task DeleteAsync(Project project, CancellationToken cancellationToken);
    Task<bool> IsNameUniqueAsync(ProjectName requestName, CancellationToken cancellationToken);
}