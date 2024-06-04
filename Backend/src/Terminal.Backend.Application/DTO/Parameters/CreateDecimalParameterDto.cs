using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO.Parameters;

public sealed record CreateDecimalParameterDto(ParameterId Id, string Name, string Unit, decimal Step)
{
    public Parameter AsParameter() => new DecimalParameter(this.Id, this.Name, this.Unit, this.Step);
}