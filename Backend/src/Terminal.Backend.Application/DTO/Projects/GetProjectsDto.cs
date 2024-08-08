namespace Terminal.Backend.Application.DTO.Projects;

public class GetProjectsDto
{
    public IEnumerable<ProjectDto> Projects { get; set; } = [];
    public int TotalCount { get; set; }

    public record ProjectDto(Guid Id, string Name);
}
