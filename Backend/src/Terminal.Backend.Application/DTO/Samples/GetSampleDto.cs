using Terminal.Backend.Application.DTO.Recipes;

namespace Terminal.Backend.Application.DTO.Samples;

public class GetSampleDto
{
    public GetSampleDto(Guid Id, string Code, GetRecipeDto? Recipe, string CreatedAtUtc, string? Comment,
        Guid ProjectId, IEnumerable<GetSampleStepsDto> Steps, IEnumerable<string> Tags)
    {
        this.Id = Id;
        this.Code = Code;
        this.Recipe = Recipe;
        this.CreatedAtUtc = CreatedAtUtc;
        this.Comment = Comment;
        this.ProjectId = ProjectId;
        this.Steps = Steps;
        this.Tags = Tags;
    }

    public Guid Id { get; set; }
    public string Code { get; set; }
    public GetRecipeDto? Recipe { get; set; }
    public string CreatedAtUtc { get; set; }
    public string? Comment { get; set; }
    public Guid ProjectId { get; set; }
    public IEnumerable<GetSampleStepsDto> Steps { get; set; }
    public IEnumerable<string> Tags { get; set; }
}