using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Users.Update.Password.WithAdminPrivileges;

internal sealed class UpdateUserPasswordAdministratorCommandHandler
    : IRequestHandler<UpdateUserPasswordAdministratorCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserPasswordAdministratorCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(UpdateUserPasswordAdministratorCommand request, CancellationToken cancellationToken)
    {
        var (id, newPassword) = request;
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.UpdatePassword(_passwordHasher.Hash(newPassword));
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}