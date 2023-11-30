using MediatR;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Commands.Sample.Create;

public sealed record CreateSampleCommand(
    Guid SampleId,
    Guid ProjectId,
    Guid? RecipeId, 
    IEnumerable<CreateSampleStepDto> Steps, 
    IEnumerable<Guid> TagIds, 
    string Comment, bool SaveAsRecipe, string? RecipeName = null) : IRequest;