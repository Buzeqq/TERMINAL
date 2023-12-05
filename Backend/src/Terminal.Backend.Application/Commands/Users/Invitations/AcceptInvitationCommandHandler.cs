using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Users.Invitations;

internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AcceptInvitationCommandHandler(IInvitationRepository invitationRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _invitationRepository = invitationRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var (id, password) = request;
        var invitation = await _invitationRepository.GetByIdAsync(id, cancellationToken);
        if (invitation is null)
        {
            throw new InvitationNotFoundExceptions();
        }

        var user = invitation.User;
        user.Activate();
        user.UpdatePassword(_passwordHasher.Hash(password));
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}