using MediatR;

namespace Terminal.Backend.Application.Ping;

public class PingQueryHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("backend reachable");
    }
}