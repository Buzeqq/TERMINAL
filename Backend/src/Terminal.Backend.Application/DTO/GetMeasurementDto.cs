using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO;

public class GetMeasurementDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public Guid? RecipeId { get; set; }
    public string CreatedAtUtc { get; set; }
    public string? Comment { get; set; }
    public Guid ProjectId { get; set; }
    public IEnumerable<CreateMeasurementStepDto> Steps { get; set; }
    public IEnumerable<string> Tags { get; set; }
}