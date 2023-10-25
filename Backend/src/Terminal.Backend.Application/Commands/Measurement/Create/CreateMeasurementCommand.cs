using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Commands.Measurement.Create;

public sealed record CreateMeasurementCommand(
    Guid MeasurementId,
    Guid? RecipeId, 
    IEnumerable<CreateMeasurementStepDto>? Steps, 
    IEnumerable<string> Tags, 
    string Comment) : IRequest;