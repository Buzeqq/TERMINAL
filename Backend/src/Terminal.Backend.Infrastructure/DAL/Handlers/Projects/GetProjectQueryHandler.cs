using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Application.Projects.Get;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Projects;

internal sealed class GetProjectQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetProjectQuery, GetProjectDto?>
{
    public async Task<GetProjectDto?> Handle(GetProjectQuery query, CancellationToken cancellationToken)
    {
        var projectId = query.Id;
        var project = await dbContext.Projects
            .TagWith("Get project with samples")
            .AsNoTracking()
            .Where(p => p.Id == projectId)
            .Select(p => new GetProjectDto(p.Id, p.Name, p.IsActive, p.Samples.Select(s => s.Id.Value)))
            .SingleOrDefaultAsync(cancellationToken);

        return project;
    }
}
