using MediatR;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Tags.Get;

public class GetTagQuery : IRequest<GetTagDto?>
{
    public Guid TagId { get; set; }
}