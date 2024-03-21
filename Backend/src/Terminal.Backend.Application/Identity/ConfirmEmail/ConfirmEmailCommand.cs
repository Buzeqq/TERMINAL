using MediatR;

namespace Terminal.Backend.Application.Identity.ConfirmEmail;

public record ConfirmEmailCommand(string UserId, string Code, string? ChangedEmail) : IRequest;