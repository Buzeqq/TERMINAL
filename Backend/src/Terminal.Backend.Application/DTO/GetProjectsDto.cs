using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO;

public class GetProjectsDto
{
    public ProjectId Id { get; set; }
    public ProjectName Name { get; set; }
}