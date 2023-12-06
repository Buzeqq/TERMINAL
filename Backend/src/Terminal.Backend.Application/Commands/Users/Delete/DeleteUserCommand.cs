using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Delete;

public sealed record DeleteUserCommand(UserId Id) : IRequest;