using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.DTO.Samples;

public class GetSampleDto
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public GetRecipeDto? Recipe { get; set; }
    public required string CreatedAtUtc { get; set; }
    public string? Comment { get; set; }
    public Guid ProjectId { get; set; }
    public IEnumerable<GetSampleStepsDto> Steps { get; set; } = [];
    public IEnumerable<GetTagsDto.TagDto> Tags { get; set; } = [];
}
