using MediatR;

namespace Terminal.Backend.Application.Commands.Users.Invitations;

public record AcceptInvitationCommand(Guid Id, string Password) : IRequest;