namespace Terminal.Backend.Application.DTO.Projects;

public record GetProjectDto(Guid Id, string Name, bool IsActive, IEnumerable<Guid> SamplesIds);
