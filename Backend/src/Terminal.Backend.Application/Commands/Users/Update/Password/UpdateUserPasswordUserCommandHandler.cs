using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Users.Update.Password;

internal sealed class UpdateUserPasswordUserCommandHandler : IRequestHandler<UpdateUserPasswordUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserPasswordUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task Handle(UpdateUserPasswordUserCommand request, CancellationToken cancellationToken)
    {
        var (id, oldPassword, newPassword) = request;
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (!_passwordHasher.Verify(oldPassword, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        user.UpdatePassword(_passwordHasher.Hash(newPassword));
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}