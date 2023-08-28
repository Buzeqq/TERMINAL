using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class PingQueryHandler : IQueryHandler<PingQuery, string>
{
    public Task<string> HandleAsync(PingQuery request, CancellationToken ct)
    {
        return Task.FromResult("backend reachable");
    }
}