using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Infrastructure.Authentication;

internal sealed class AuthorizationService : IAuthorizationService
{
    private readonly DbSet<User> _users;

    public AuthorizationService(TerminalDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(UserId userId)
    {
        var role = await _users
            .AsNoTracking()
            .Include(x => x.Role)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.Role)
            .SingleOrDefaultAsync();

        return role is null ? new HashSet<string>() : role.Permissions.Select(p => p.Name.Value).ToHashSet();
    }

    public async Task<Role> GetRole(UserId userId)
    {
        var role = await _users
            .AsNoTracking()
            .Include(x => x.Role)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.Role)
            .SingleOrDefaultAsync();

        return role ?? Role.Guest;
    }
}