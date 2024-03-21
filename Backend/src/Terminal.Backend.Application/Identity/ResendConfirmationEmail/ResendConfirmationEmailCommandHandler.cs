namespace Terminal.Backend.Application.Identity.ResendConfirmationEmail;

using Abstractions;
using MediatR;

internal sealed class ResendConfirmationEmailCommandHandler(IUserService userService) : IRequestHandler<ResendConfirmationEmailCommand>
{
    public Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        => userService.ResendConfirmationEmailAsync(request.Email);
}
