using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagsAmountQueryHandler(TerminalDbContext dbContext) : IRequestHandler<GetTagsAmountQuery, int>
{
    private readonly DbSet<Tag> _tags = dbContext.Tags;

    public Task<int> Handle(GetTagsAmountQuery request, CancellationToken cancellationToken) =>
        _tags
            .AsNoTracking()
            .CountAsync(cancellationToken);
}