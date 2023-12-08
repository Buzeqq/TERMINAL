using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.DAL.Repositories;

internal sealed class InvitationRepository : IInvitationRepository
{
    private readonly DbSet<Invitation> _invitations;

    public InvitationRepository(TerminalDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }

    public async Task AddAsync(Invitation invitation, CancellationToken ct)
        => await _invitations.AddAsync(invitation, ct);

    public async Task DeleteAllForUserAsync(User user, CancellationToken ct)
    {
        var invitations = await _invitations
            .Where(i => i.User == user)
            .ToListAsync(ct);
        _invitations.RemoveRange(invitations);
    }

    public Task<Invitation?> GetByIdAsync(InvitationId id, CancellationToken ct) =>
        _invitations
            .Include(i => i.User)
            .SingleOrDefaultAsync(i => i.Id.Equals(id), ct);
}