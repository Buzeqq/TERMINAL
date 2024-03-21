using MediatR;

namespace Terminal.Backend.Application.Identity.ConfirmEmail;

using Core.ValueObjects;

public record ConfirmEmailCommand(string UserId, string Code, Email? ChangedEmail) : IRequest;
