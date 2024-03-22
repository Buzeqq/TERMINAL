namespace Terminal.Backend.Application.Identity.ResetPassword;

using Abstractions;
using MediatR;

internal sealed class ResetPasswordCommandHandler(IUserService userService) : IRequestHandler<ResetPasswordCommand>
{
    public Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        => userService.ResetPasswordAsync(request.Email, request.NewPassword, request.Code);
}
