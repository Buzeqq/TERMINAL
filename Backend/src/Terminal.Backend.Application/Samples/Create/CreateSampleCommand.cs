using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Create;

public record CreateSampleCommand(
    SampleId SampleId,
    ProjectId ProjectId,
    RecipeId? RecipeId,
    IEnumerable<CreateSampleStepDto> Steps,
    IEnumerable<TagId> TagIds,
    Comment Comment,
    bool SaveAsRecipe,
    RecipeName? RecipeName = null) : IRequest;
