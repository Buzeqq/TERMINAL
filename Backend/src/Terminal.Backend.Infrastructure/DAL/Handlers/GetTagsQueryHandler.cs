using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetTagsQueryHandler : IQueryHandler<GetMostPopularTagsQuery, IEnumerable<GetTagsDto>>
{
    private readonly DbSet<Measurement> _measurements;

    public GetTagsQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
    }

    public async Task<IEnumerable<GetTagsDto>> HandleAsync(GetMostPopularTagsQuery query, CancellationToken ct)
        => await _measurements
            .AsNoTracking()
            .SelectMany(m => m.Tags)
            .GroupBy(t => t)
            .OrderByDescending(g => g.Count())
            .Take(query.Count)
            .Select(g => g.Key.AsDto())
            .ToListAsync(ct);
}