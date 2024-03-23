namespace Terminal.Backend.Application.Projects.Get;

public sealed class GetProjectsAmountQuery(bool onlyActive = true) : IRequest<int>
{
    public bool OnlyActive { get; set; } = onlyActive;
}