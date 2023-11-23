using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Factories;

public interface IInvitationFactory
{
    Task<Invitation> CrateAsync(User user, CancellationToken ct);
}