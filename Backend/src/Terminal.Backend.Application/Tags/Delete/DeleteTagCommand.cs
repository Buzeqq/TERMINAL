using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Delete;

public sealed record DeleteTagCommand(TagId Id) : IRequest;