namespace Terminal.Backend.Application.Identity.ForgotPassword;

using Core.ValueObjects;

public record ForgotPasswordCommand(Email Email) : IRequest;
