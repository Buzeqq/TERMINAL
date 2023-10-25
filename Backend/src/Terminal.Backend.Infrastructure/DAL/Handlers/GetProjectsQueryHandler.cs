using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<GetProjectsDto>>
{
    private readonly TerminalDbContext _dbContext;

    public GetProjectsQueryHandler(TerminalDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<GetProjectsDto>> Handle(GetProjectsQuery request,
        CancellationToken ct)
        => await _dbContext
            .Projects
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => x.AsGetProjectsDto())
            .ToListAsync(ct);
}