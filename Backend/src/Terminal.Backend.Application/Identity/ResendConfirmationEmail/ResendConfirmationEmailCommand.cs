namespace Terminal.Backend.Application.Identity.ResendConfirmationEmail;

public record ResendConfirmationEmailCommand(string Email) : IRequest;
