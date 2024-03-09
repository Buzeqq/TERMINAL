using MediatR;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Parameters.Get;

public sealed class GetParameterQuery : IRequest<Parameter?>
{
    public required ParameterId Id;
}