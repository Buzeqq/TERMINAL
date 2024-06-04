using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Samples.Search;

public sealed class SearchSampleQuery(string phrase, int pageNumber, int pageSize) : IRequest<GetSamplesDto>
{
    public string SearchPhrase { get; set; } = phrase;

    public PagingParameters Parameters { get; set; } = new() { PageNumber = pageNumber, PageSize = pageSize };
}