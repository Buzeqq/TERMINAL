namespace Terminal.Backend.Application.DTO.ParameterValues;

public sealed record GetSampleDecimalParameterValueDto(string Name, decimal Value, string Unit)
    : GetSampleNumericParameterValueDto(Name, Unit);