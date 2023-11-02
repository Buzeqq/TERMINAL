using MediatR;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Parameters;

namespace Terminal.Backend.Application.Queries;

public sealed class SearchMeasurementQuery : IRequest<GetMeasurementsDto>
{
    public string SearchPhrase { get; set; }
    
    public PagingParameters Parameters { get; set; }
    
    public SearchMeasurementQuery(string phrase, int pageNumber, int pageSize)
    {
        SearchPhrase = phrase;
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
    }
}