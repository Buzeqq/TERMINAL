using MediatR;

namespace Terminal.Backend.Application.Ping;

public class PingQuery : IRequest<string> { }