using MediatR;

namespace Terminal.Backend.Application.Commands.Users.Create;

public sealed record CreateUserCommand(string Email, string Role) : IRequest;