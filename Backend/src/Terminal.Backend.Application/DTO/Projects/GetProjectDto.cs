namespace Terminal.Backend.Application.DTO.Projects;

public class GetProjectDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<Guid> SamplesIds { get; set; } = [];
}