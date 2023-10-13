using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class GetProjectQuery : IRequest<GetProjectDto?>
{
    public Guid ProjectId { get; set; }
}