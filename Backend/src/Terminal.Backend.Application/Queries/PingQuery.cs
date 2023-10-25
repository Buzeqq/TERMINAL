using MediatR;

namespace Terminal.Backend.Application.Queries;

public sealed class PingQuery : IRequest<string>
{
}