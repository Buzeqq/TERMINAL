namespace Terminal.Backend.Application.Identity.ForgotPassword;

using Abstractions;
using MediatR;

internal sealed class ForgotPasswordCommandHandler(IUserService userService) : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        => await userService.ForgotPasswordAsync(request.Email);
}
