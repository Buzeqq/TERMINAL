using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Queries.Projects.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectsAmountQueryHandler : IRequestHandler<GetProjectsAmountQuery, int>
{
    private readonly DbSet<Project> _projects;

    public GetProjectsAmountQueryHandler(TerminalDbContext dbContext)
    {
        _projects = dbContext.Projects;
    }

    public async Task<int> Handle(GetProjectsAmountQuery request, CancellationToken cancellationToken)
    {
        var amount = _projects
            .AsNoTracking()
            .Count();

        return amount;
    }
}