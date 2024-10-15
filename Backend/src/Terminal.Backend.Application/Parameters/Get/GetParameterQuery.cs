using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Parameters.Get;

public record GetParameterQuery(ParameterId Id) : IRequest<Parameter?>;
