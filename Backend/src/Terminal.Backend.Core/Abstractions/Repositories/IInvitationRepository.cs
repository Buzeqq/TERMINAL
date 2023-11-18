using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Repositories;

public interface IInvitationRepository
{
    Task AddAsync(Invitation invitation, CancellationToken ct);
}