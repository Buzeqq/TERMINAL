using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Queries.Tags.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Tags;

internal sealed class GetTagsAmountQueryHandler : IRequestHandler<GetTagsAmountQuery, int>
{
    private readonly DbSet<Tag> _tags;

    public GetTagsAmountQueryHandler(TerminalDbContext dbContext)
    {
        _tags = dbContext.Tags;
    }

    public async Task<int> Handle(GetTagsAmountQuery request, CancellationToken cancellationToken)
    {
        var amount = _tags
            .AsNoTracking()
            .Count();

        return amount;
    }
}