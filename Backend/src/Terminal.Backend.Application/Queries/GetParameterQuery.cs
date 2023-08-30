using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Queries;

public sealed class GetParameterQuery : IQuery<Parameter?>
{
    public ParameterName Name;
}