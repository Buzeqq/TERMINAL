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

    public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken ct)
        => await _users
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == email, ct);

    public async Task AddUserAsync(User user, CancellationToken ct)
        => await _users
            .AddAsync(user, ct);
}