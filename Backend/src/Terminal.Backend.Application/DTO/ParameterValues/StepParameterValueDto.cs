using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO.ParameterValues;

public abstract record StepParameterValueDto(ParameterId ParameterId);

public sealed record StepTextParameterValueDto(ParameterId ParameterId, string Value) : StepParameterValueDto(ParameterId);
public sealed record StepIntegerParameterValueDto(ParameterId ParameterId, int Value) : StepParameterValueDto(ParameterId);
public sealed record StepDecimalParameterValueDto(ParameterId ParameterId, decimal Value) : StepParameterValueDto(ParameterId);


