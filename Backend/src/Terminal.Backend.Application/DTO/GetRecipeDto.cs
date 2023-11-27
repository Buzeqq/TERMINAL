namespace Terminal.Backend.Application.DTO;

public sealed record GetRecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<GetSampleStepsDto> Steps { get; set; }
}