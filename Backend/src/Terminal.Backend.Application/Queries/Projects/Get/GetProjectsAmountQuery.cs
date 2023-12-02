using MediatR;

namespace Terminal.Backend.Application.Queries.Projects.Get;

public sealed class GetProjectsAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}