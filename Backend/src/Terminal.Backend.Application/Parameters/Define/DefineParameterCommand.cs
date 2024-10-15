using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.Parameters.Define;

public record DefineParameterCommand(Parameter Parameter) : IRequest;
