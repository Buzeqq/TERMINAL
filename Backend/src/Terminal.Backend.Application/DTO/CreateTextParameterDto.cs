using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO;

public sealed record CreateTextParameterDto(ParameterId Id, string Name, List<string> AllowedValues)
{
    public TextParameter AsParameter() => new(Id, Name, AllowedValues);
}