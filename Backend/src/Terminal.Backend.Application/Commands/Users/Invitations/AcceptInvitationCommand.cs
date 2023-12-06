using MediatR;

namespace Terminal.Backend.Application.Commands.Users.Invitations;

public sealed record AcceptInvitationCommand(Guid Id, string Password) : IRequest;