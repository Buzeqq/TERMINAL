using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName, CancellationToken ct);
}