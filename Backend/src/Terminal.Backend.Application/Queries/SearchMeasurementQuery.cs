using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Queries;

public sealed class SearchMeasurementQuery : IRequest<GetMeasurementsDto>
{
    public string SearchPhrase { get; set; }
    
    public SearchMeasurementQuery(string phrase)
    {
        SearchPhrase = phrase;
    }
}