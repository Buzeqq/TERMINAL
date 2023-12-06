using MediatR;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Users.Delete;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        await _userRepository.DeleteAsync(user, cancellationToken);
    }
}