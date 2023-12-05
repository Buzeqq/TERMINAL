using MediatR;

namespace Terminal.Backend.Application.Queries.Projects.Get;

public sealed class GetProjectsAmountQuery : IRequest<int>
{
    public GetProjectsAmountQuery(bool onlyActive = true)
    {
        OnlyActive = onlyActive;
    }

    public bool OnlyActive { get; set; }
}