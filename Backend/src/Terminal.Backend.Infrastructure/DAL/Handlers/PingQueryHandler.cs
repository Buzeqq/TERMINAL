using MediatR;
using Terminal.Backend.Application.Queries;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public class PingQueryHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("backend reachable");
    }
}