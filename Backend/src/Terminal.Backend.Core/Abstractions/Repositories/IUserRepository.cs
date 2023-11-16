using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(Email email, CancellationToken ct);
}