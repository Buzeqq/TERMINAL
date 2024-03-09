using MediatR;

namespace Terminal.Backend.Application.Users.Login;

public sealed record LoginCommand(string Email, string Password, string? TwoFactorCode, string? TwoFactorRecoveryCode) 
    : IRequest<string>;