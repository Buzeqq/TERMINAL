namespace Terminal.Backend.Application.DTO;

public class GetProjectsDto
{
    public IEnumerable<ProjectDto> Projects { get; set; }

    public record ProjectDto(Guid Id, string Name);
}