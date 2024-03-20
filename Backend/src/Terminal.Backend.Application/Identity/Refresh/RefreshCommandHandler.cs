using MediatR;
using Terminal.Backend.Application.Abstractions;

namespace Terminal.Backend.Application.Identity.Refresh;

internal sealed class RefreshCommandHandler(IUserService userService) : IRequestHandler<RefreshCommand, string>
{
    public async Task<string> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        await userService.RefreshTokenAsync(request.RefreshToken);
        return string.Empty;
    }
}