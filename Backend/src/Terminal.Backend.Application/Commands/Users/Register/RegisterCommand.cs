using MediatR;

namespace Terminal.Backend.Application.Commands.Users.Register;

public sealed record RegisterUserCommand(string Email, string Password) : IRequest;