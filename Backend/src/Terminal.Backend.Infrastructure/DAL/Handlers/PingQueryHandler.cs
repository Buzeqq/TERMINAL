using MediatR;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

internal sealed class PingQueryHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken ct)
    {
        return Task.FromResult("backend reachable");
    }
}