using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries.Tags.Get;

public class GetTagQuery : IRequest<GetTagDto?>
{
    public Guid TagId { get; set; }
}