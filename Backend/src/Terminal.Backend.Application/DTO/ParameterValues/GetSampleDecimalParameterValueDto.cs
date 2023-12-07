namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record GetSampleDecimalParameterValueDto(Guid Id, string Name, decimal Value, string Unit)
    : GetSampleNumericParameterValueDto(Id, Name, Unit);