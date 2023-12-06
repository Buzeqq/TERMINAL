using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Users.Invitations;
using Terminal.Backend.Application.Queries.Users.Invitations;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Users.Invitations;

internal sealed class CheckInvitationQueryHandler : IRequestHandler<CheckInvitationQuery, GetInvitationDto?>
{
    private readonly DbSet<Invitation> _invitations;

    public CheckInvitationQueryHandler(TerminalDbContext dbContext)
    {
        _invitations = dbContext.Invitations;
    }


    public async Task<GetInvitationDto?> Handle(CheckInvitationQuery request, CancellationToken cancellationToken)
    {
        var invitation = await _invitations
            .Include(i => i.User)
            .SingleOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken);
        return invitation.User.Activated ? null : invitation.AsGetInvitationDto();
    }
}