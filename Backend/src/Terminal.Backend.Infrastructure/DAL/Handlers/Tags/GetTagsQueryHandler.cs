using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagsQueryHandler(TerminalDbContext dbContext) : IRequestHandler<GetTagsQuery, GetTagsDto>
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public async Task<GetTagsDto> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tagsQuery = _tags
            .AsNoTracking()
            .Select(t => new GetTagsDto.TagDto(t.Id, t.Name));

        var totalCount = await tagsQuery.CountAsync(cancellationToken);

        var tags = await tagsQuery.OrderBy(request.OrderingParameters)
            .Paginate(request.PagingParameters)
            .ToListAsync(cancellationToken);

        return new GetTagsDto(tags, totalCount, request.PagingParameters);
    }
}
