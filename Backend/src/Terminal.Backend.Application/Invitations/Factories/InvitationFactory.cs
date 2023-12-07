using Microsoft.Extensions.Options;
using Terminal.Backend.Core.Abstractions.Factories;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Invitations.Factories;

internal sealed class InvitationFactory : IInvitationFactory
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IOptions<InvitationOptions> _invitationOptions;

    public InvitationFactory(IInvitationRepository invitationRepository, IOptions<InvitationOptions> invitationOptions)
    {
        _invitationRepository = invitationRepository;
        _invitationOptions = invitationOptions;
    }

    public async Task<Invitation> CrateAsync(User user, CancellationToken ct)
    {
        await _invitationRepository.DeleteAllForUserAsync(user, ct);
        var id = InvitationId.Create();

        var invitation = Invitation.CreateFor(user, id, CreateLink(id), DateTime.UtcNow.AddHours(1));
        await _invitationRepository.AddAsync(invitation, ct);

        return invitation;
    }

    private InvitationLink CreateLink(InvitationId id) =>
        new(Path.Join(_invitationOptions.Value.LinkBase, id.Value.ToString("D")));
}