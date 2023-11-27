using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class GetTagsQueryHandler : 
    // IRequestHandler<GetMostPopularTagsQuery, GetTagsDto>,
    IRequestHandler<GetTagsQuery, GetTagsDto>
{
    private readonly DbSet<Sample> _measurements;
    private readonly DbSet<Tag> _tags;

    public GetTagsQueryHandler(TerminalDbContext dbContext)
    {
        _measurements = dbContext.Measurements;
        _tags = dbContext.Tags;
    }
    
    // public async Task<GetTagsDto> Handle(GetMostPopularTagsQuery query, CancellationToken ct)
    //     => (await _measurements
    //         .AsNoTracking()
    //         .SelectMany(m => m.Tags)
    //         .GroupBy(t => t)
    //         .OrderByDescending(g => g.Count())
    //         .Take(query.Count)
    //         .Select(g => g.Key)
    //         .ToListAsync(ct)).AsGetTagsDto();

    public async Task<GetTagsDto> Handle(GetTagsQuery query, CancellationToken ct)
        => (await _tags
            .AsNoTracking()
            .Where(t => t.IsActive)
            .Paginate(query.Parameters)
            .ToListAsync(ct)).AsGetTagsDto();
}