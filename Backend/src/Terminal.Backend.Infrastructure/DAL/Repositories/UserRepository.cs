using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Repositories;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(TerminalDbContext context)
    {
        _users = context.Users;
    }

    public async Task<User?> GetUserByEmail(Email email, CancellationToken ct)
        => await _users.SingleOrDefaultAsync(u => u.Email == email, ct);
}