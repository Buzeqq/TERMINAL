using MediatR;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Samples.Get;

public sealed class GetSamplesQuery : IRequest<GetSamplesDto>
{
    public PagingParameters Parameters { get; set; }

    public GetSamplesQuery(int pageNumber, int pageSize)
    {
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
    }
}