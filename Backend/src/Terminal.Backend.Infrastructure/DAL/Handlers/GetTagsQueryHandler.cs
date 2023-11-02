using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetTagsQueryHandler : 
    IRequestHandler<GetMostPopularTagsQuery, GetTagsDto>, 
    IRequestHandler<GetTagsQuery, GetTagsDto>
{
    private readonly DbSet<Measurement> _measurements;
    private readonly DbSet<Tag> _tags;

    public GetTagsQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
        _tags = dbContext.Tags;
    }

    public async Task<GetTagsDto> Handle(GetMostPopularTagsQuery query, CancellationToken ct)
        => new()
        {
            Tags = await _measurements
                .AsNoTracking()
                .SelectMany(m => m.Tags)
                .GroupBy(t => t)
                .OrderByDescending(g => g.Count())
                .Take(query.Count)
                .Select(g => g.Key.Name.Value)
                .ToListAsync(ct)
        };

    public async Task<GetTagsDto> Handle(GetTagsQuery query, CancellationToken ct) 
        => new()
        {
            Tags = await _tags
                .AsNoTracking()
                .Select(t => t.Name.Value)
                .Paginate(query.Parameters)
                .ToListAsync(ct)
        };
}