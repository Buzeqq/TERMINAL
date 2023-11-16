using MediatR;

namespace Terminal.Backend.Application.Commands.Users.Register;

public sealed record RegisterCommand(string Email, string Password) : IRequest;