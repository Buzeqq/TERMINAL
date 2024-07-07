using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Projects.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectsAmountQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetProjectsAmountQuery, int>
{
    private readonly DbSet<Project> _projects = dbContext.Projects;

    public Task<int> Handle(GetProjectsAmountQuery request, CancellationToken cancellationToken) =>
        _projects
            .AsNoTracking()
            .CountAsync(cancellationToken);
}