using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(Email email, CancellationToken ct);
    Task AddUserAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
}