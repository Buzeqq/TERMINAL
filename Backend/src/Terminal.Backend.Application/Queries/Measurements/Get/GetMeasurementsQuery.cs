using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Measurements.Get;

public sealed class GetMeasurementsQuery : IRequest<GetMeasurementsDto>
{
    public PagingParameters Parameters { get; set; }

    public GetMeasurementsQuery(int pageNumber, int pageSize)
    {
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
    }
}