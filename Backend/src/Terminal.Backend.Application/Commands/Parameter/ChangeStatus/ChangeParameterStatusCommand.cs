using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Parameter.ChangeStatus;

public sealed record ChangeParameterStatusCommand(ParameterId Name, bool Status) : IRequest;