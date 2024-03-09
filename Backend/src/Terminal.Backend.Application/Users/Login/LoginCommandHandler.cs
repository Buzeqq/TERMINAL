using MediatR;

namespace Terminal.Backend.Application.Users.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(string.Empty);
    }
}