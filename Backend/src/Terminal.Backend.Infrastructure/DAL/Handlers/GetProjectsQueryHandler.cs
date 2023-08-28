using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery, IEnumerable<GetProjectsDto>>
{
    private readonly TerminalDbContext _dbContext;

    public GetProjectsQueryHandler(TerminalDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<GetProjectsDto>> HandleAsync(GetProjectsQuery request,
        CancellationToken ct)
        => await _dbContext
            .Projects
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => x.AsDto())
            .ToListAsync(ct);
}