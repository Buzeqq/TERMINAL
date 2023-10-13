using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class GetMostPopularTagsQuery : IRequest<GetTagsDto>
{
    // TODO: read from config file max count
    public int Count { get; set; }
}