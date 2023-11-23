using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Application.Queries;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class SearchTagQueryHandler : IRequestHandler<SearchTagQuery, GetTagsDto>
{
    private readonly DbSet<Tag> _tags;

    public SearchTagQueryHandler(TerminalDbContext dbContext)
    {
        _tags = dbContext.Tags;
    }

    public async Task<GetTagsDto> Handle(SearchTagQuery request, CancellationToken ct)
        => (await _tags
                .AsNoTracking()
                .Where(t => EF.Functions.ILike(t.Name, $"%{request.SearchPhrase}%"))
                .Select(t => t)
                .ToListAsync(ct)).AsGetTagsDto();
}