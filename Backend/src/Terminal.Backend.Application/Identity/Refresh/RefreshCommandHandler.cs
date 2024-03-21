using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.Refresh;

internal sealed class RefreshCommandHandler(IUserService userService) : IRequestHandler<RefreshCommand>
{
    public async Task Handle(RefreshCommand request, CancellationToken cancellationToken) => await userService.RefreshTokenAsync(request.RefreshToken);
}