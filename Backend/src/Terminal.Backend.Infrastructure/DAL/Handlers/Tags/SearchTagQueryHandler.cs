using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Tags.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class SearchTagQueryHandler(TerminalDbContext dbContext) : IRequestHandler<SearchTagQuery, GetTagsDto>
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public async Task<GetTagsDto> Handle(SearchTagQuery request, CancellationToken ct)
        => (await _tags
            .AsNoTracking()
            .Where(t => EF.Functions.ILike(t.Name, $"%{request.SearchPhrase}%"))
            .Select(t => t)
            .ToListAsync(ct)).AsGetTagsDto();
}