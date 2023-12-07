using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(TerminalDbContext context)
    {
        _users = context.Users;
    }

    public Task<User?> GetUserByEmailAsync(Email email, CancellationToken ct)
        => _users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == email, ct);

    public async Task AddUserAsync(User user, CancellationToken ct)
        => await _users.AddAsync(user, ct);

    public Task UpdateAsync(User user, CancellationToken ct)
    {
        _users.Update(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetAsync(UserId id, CancellationToken cancellationToken)
        => _users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }
}