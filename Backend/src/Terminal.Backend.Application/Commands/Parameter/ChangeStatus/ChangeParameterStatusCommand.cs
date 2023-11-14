using MediatR;

namespace Terminal.Backend.Application.Commands.Parameter.ChangeStatus;

public sealed record ChangeParameterStatusCommand(Guid Name, bool Status) : IRequest;