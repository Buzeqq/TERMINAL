using MediatR;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Queries.Tags.Get;

public class GetTagQuery : IRequest<GetTagDto?>
{
    public Guid TagId { get; set; }
}