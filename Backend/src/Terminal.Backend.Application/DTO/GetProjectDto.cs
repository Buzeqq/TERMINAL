using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.DTO;

public class GetProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<Measurement> Measurements { get; set; }
}