using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Identity.ResendConfirmationEmail;

public record ResendConfirmationEmailCommand(Email Email) : IRequest;
