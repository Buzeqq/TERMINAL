namespace Terminal.Backend.Application.DTO;

public class GetProjectsDto
{
    public IEnumerable<InnerProjectDto> Projects { get; set; }

    public record InnerProjectDto(Guid Id, string Name);
}