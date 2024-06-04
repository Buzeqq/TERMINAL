namespace Terminal.Backend.Application.Tags.Get;

public sealed class GetTagsAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}