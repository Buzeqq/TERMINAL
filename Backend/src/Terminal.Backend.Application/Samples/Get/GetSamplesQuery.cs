using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Samples.Get;

public record GetSamplesQuery(
    string? SearchPhrase,
    PagingParameters PagingParameters,
    OrderingParameters OrderingParameters)
    : IRequest<GetSamplesDto>;
