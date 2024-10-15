using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.ChangeStatus;

public record ChangeTagStatusCommand(TagId Id, bool IsActive) : IRequest;
