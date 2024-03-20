using MediatR;

namespace Terminal.Backend.Application.Identity.Refresh;

public record RefreshCommand(string RefreshToken) : IRequest<string>;