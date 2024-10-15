using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Delete;

public record DeleteTagCommand(TagId Id) : IRequest;
