using MediatR;

namespace Terminal.Backend.Application.Queries.Samples.Get;

public sealed class GetSamplesAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}