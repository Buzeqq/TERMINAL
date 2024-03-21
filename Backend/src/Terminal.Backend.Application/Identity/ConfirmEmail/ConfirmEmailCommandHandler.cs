using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(IUserService userService) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var (userId, code, newEmail) = request;
        await userService.ConfirmEmailAsync(userId, code, newEmail);
    }
}