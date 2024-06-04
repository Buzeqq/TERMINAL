using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Samples.Get;

public sealed class GetSamplesQuery(int pageNumber, int pageSize, string orderBy, bool desc)
    : IRequest<GetSamplesDto>
{
    public PagingParameters Parameters { get; set; } = new() { PageNumber = pageNumber, PageSize = pageSize };
    public OrderingParameters OrderingParameters { get; set; } = new() { OrderBy = orderBy, Desc = desc };
}