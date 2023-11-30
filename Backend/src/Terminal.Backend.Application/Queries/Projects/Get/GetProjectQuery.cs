using MediatR;
using Terminal.Backend.Application.DTO.Projects;

namespace Terminal.Backend.Application.Queries.Projects.Get;

public sealed class GetProjectQuery : IRequest<GetProjectDto?>
{
    public Guid ProjectId { get; set; }
}