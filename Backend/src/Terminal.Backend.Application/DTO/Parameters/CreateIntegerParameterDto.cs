using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO.Parameters;

public sealed record CreateIntegerParameterDto(ParameterId Id, string Name, string Unit, int Step)
{
    public Parameter AsParameter() => new IntegerParameter(Id, Name, Unit, Step);
}