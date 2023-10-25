using MediatR;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Queries;

public sealed class GetParameterQuery : IRequest<Parameter?>
{
    public ParameterName Name;
}