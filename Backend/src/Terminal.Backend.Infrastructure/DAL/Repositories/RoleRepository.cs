using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Commands.Users.Create;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class RoleRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;

    public RoleRepository(TerminalDbContext dbContext)
    {
        _roles = dbContext.Roles;
    }

    public async Task<Role?> GetByNameAsync(string roleName, CancellationToken ct)
        => await _roles
            .SingleOrDefaultAsync(r => string.Equals(r.Name.ToLower(), roleName.ToLower()), ct);
}