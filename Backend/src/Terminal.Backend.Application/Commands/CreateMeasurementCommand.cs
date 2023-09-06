using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Commands;

public sealed record CreateMeasurementCommand(
    Guid MeasurementId,
    Guid? RecipeId, 
    IEnumerable<CreateMeasurementStepDto>? Steps, 
    IEnumerable<string> Tags, 
    string Comment) : ICommand;