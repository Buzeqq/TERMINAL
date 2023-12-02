using MediatR;

namespace Terminal.Backend.Application.Queries.Tags.Get;

public sealed class GetTagsAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}