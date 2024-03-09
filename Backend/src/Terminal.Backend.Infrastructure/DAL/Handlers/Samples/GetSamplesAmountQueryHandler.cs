using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Samples.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Samples;

internal sealed class GetSamplesAmountQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetSamplesAmountQuery, int>
{
    private readonly DbSet<Sample> _samples = dbContext.Samples;

    public Task<int> Handle(GetSamplesAmountQuery request, CancellationToken ct)
    {
        return _samples
            .AsNoTracking()
            .CountAsync(ct);
    }
}