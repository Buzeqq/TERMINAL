using MediatR;

namespace Terminal.Backend.Application.Users.Register;

public sealed record RegisterCommand(string Email, string Password) : IRequest;