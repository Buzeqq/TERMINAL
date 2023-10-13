using MediatR;

namespace Terminal.Backend.Application.Commands.Tag.Create;

public sealed record CreateTagCommand(string Name) : IRequest;