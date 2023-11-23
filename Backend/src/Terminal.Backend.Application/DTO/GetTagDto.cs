using MediatR;

namespace Terminal.Backend.Application.DTO;

public class GetTagDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}