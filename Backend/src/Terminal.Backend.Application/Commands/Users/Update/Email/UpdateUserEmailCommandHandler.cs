using MediatR;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;

namespace Terminal.Backend.Application.Commands.Users.Update.Email;

internal sealed class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var (id, email) = request;
        var user = await _userRepository.GetAsync(id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.UpdateEmail(email);
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}