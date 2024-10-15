using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Tags;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagQueryHandler(TerminalDbContext dbContext) : IRequestHandler<GetTagQuery, GetTagDto?>
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public async Task<GetTagDto?> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tagId = request.Id;
        var tag = await _tags
            .AsNoTracking()
            .Where(t => t.Id == tagId)
            .Select(t => new GetTagDto(t.Id, t.Name, t.IsActive))
            .SingleOrDefaultAsync(cancellationToken);

        return tag;
    }
}
