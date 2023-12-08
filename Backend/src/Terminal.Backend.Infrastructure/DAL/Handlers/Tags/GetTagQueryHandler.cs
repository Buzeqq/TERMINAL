using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Queries.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagQueryHandler : IRequestHandler<GetTagQuery, GetTagDto?>
{
    private readonly DbSet<Tag> _tags;

    public GetTagQueryHandler(TerminalDbContext dbContext)
    {
        _tags = dbContext.Tags;
    }

    public async Task<GetTagDto?> Handle(GetTagQuery request, CancellationToken ct)
    {
        var tagId = request.TagId;
        var tag = (await _tags
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id.Equals(tagId), ct))?.AsGetTagDto();

        return tag;
    }
}