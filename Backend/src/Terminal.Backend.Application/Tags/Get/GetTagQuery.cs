using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Get;

public record GetTagQuery(TagId Id) : IRequest<GetTagDto?>;
