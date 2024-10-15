using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Create;

public sealed record CreateTagCommand(
    TagId Id,
    TagName Name) : IRequest;
