using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.DTO;

public sealed record CreateTextParameterDto(string Name, IEnumerable<string> AllowedValues)
{
    public TextParameter AsParameter() => new(Name, AllowedValues);
}