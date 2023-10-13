using MediatR;

namespace Terminal.Backend.Application.Commands.Parameter.ChangeStatus;

public sealed record ChangeParameterStatusCommand(string Name, bool Status) : IRequest;