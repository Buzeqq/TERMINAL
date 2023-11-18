using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Abstractions.Factories;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Create;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, InvitationDto>
{
    private readonly ITemporaryPasswordGenerator _temporaryPasswordGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IInvitationFactory _invitationFactory;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IMailService _mailService;

    public CreateUserCommandHandler(ITemporaryPasswordGenerator temporaryPasswordGenerator,
        IUserRepository userRepository, IInvitationFactory invitationFactory,
        IInvitationRepository invitationRepository, IMailService mailService)
    {
        _temporaryPasswordGenerator = temporaryPasswordGenerator;
        _userRepository = userRepository;
        _invitationFactory = invitationFactory;
        _invitationRepository = invitationRepository;
        _mailService = mailService;
    }

    public async Task<InvitationDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (email, role) = request;
        var temporaryPassword = _temporaryPasswordGenerator.Generate();
        var newUser = new User(UserId.Create(), email, temporaryPassword.PasswordHashed, false);
        var newUserRole = Role.FromName(role) ?? throw new RoleNotFoundException(role);
        newUser.SetRole(newUserRole);
        
        await _userRepository.AddUserAsync(newUser, cancellationToken);
        var invitation = _invitationFactory.Crate(newUser);
        await _invitationRepository.AddAsync(invitation, cancellationToken);
        await _mailService.SendInvitation(invitation);

        return new InvitationDto(invitation.Link, invitation.ExpiresIn.ToString("d"));
    }
}