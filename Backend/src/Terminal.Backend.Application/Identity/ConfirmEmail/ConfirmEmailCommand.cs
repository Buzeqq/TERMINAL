namespace Terminal.Backend.Application.Identity.ConfirmEmail;

using Core.ValueObjects;

public record ConfirmEmailCommand(UserId Id, string Code, Email? NewEmail) : IRequest;
