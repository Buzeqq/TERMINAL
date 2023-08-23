using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public class PingQueryHandler : IQueryHandler<PingQuery, string>
{
    public Task<string> HandleAsync(PingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("backend reachable");
    }
}