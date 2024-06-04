using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Parameters.ChangeStatus;

public sealed record ChangeParameterStatusCommand(ParameterId Name, bool Status) : IRequest;