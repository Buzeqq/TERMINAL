using MediatR;

namespace Terminal.Backend.Application.Queries;

public class PingQuery : IRequest<string> { }