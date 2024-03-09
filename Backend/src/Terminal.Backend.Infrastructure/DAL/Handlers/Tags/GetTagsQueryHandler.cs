using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagsQueryHandler(TerminalDbContext dbContext) :
    // IRequestHandler<GetMostPopularTagsQuery, GetTagsDto>,
    IRequestHandler<GetTagsQuery, GetTagsDto>
{
    private readonly DbSet<Sample> _samples = dbContext.Samples;
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    // public async Task<GetTagsDto> Handle(GetMostPopularTagsQuery query, CancellationToken ct)
    //     => (await _samples
    //         .AsNoTracking()
    //         .SelectMany(m => m.Tags)
    //         .GroupBy(t => t)
    //         .OrderByDescending(g => g.Count())
    //         .Take(query.Count)
    //         .Select(g => g.Key)
    //         .ToListAsync(ct)).AsGetTagsDto();

    public async Task<GetTagsDto> Handle(GetTagsQuery request, CancellationToken ct)
        => (await _tags
            .AsNoTracking()
            .Where(t => t.IsActive || t.IsActive == request.OnlyActive)
            .OrderBy(request.OrderingParameters)
            .Paginate(request.Parameters)
            .ToListAsync(ct)).AsGetTagsDto();
}