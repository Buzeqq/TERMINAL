using MediatR;
using Terminal.Backend.Application.DTO.Users.Invitations;

namespace Terminal.Backend.Application.Queries.Users.Invitations;

public record CheckInvitationQuery(Guid Id) : IRequest<GetInvitationDto?>;