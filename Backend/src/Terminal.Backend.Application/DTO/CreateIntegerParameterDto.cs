using Terminal.Backend.Core.Entities.Parameters;

namespace Terminal.Backend.Application.DTO;

public sealed record CreateIntegerParameterDto(string Name, string Unit, int Step)
{
    public Parameter AsParameter() => new IntegerParameter(Name, Unit, Step);
}