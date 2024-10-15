using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Update;

public record UpdateTagCommand(TagId Id, TagName Name) : IRequest;
