using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.DTO;

public sealed record CreateDecimalParameterDto(string Name, string Unit, decimal Step)
{
    public Parameter AsParameter() => new DecimalParameter(Name, Unit, Step);
}