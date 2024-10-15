using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Projects.ChangeStatus;

public record ChangeProjectStatusCommand(ProjectId ProjectId, bool IsActive) : IRequest;
