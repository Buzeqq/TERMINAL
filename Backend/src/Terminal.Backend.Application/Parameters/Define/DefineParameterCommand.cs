using MediatR;

namespace Terminal.Backend.Application.Parameters.Define;

public sealed record DefineParameterCommand(Core.Entities.Parameters.Parameter Parameter) : IRequest;