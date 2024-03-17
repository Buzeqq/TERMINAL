using MediatR;

namespace Terminal.Backend.Application.Identity.Register;

public sealed record RegisterCommand(string Email, string Password) : IRequest;