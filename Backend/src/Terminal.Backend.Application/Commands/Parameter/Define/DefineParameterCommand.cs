using MediatR;

namespace Terminal.Backend.Application.Commands.Parameter.Define;

public sealed record DefineParameterCommand(Core.Entities.Parameters.Parameter Parameter) : IRequest;