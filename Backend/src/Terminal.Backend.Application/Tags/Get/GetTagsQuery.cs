using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.Tags;

namespace Terminal.Backend.Application.Tags.Get;

public record GetTagsQuery(PagingParameters PagingParameters, OrderingParameters OrderingParameters)
    : IRequest<GetTagsDto>;
