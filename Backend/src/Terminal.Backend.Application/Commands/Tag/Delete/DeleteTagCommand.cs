using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.Delete;

public sealed record DeleteTagCommand(TagId Id) : IRequest;