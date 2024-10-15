using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Update;

public record UpdateSampleCommand(
    SampleId Id,
    ProjectId ProjectId,
    RecipeId? RecipeId,
    IEnumerable<UpdateSampleStepDto> Steps,
    IEnumerable<TagId> TagIds,
    Comment Comment) : IRequest;
