using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IInvitationRepository
{
    Task AddAsync(Invitation invitation, CancellationToken ct);
    Task DeleteAllForUserAsync(User user, CancellationToken ct);
    Task<Invitation?> GetByIdAsync(InvitationId id, CancellationToken ct);
}