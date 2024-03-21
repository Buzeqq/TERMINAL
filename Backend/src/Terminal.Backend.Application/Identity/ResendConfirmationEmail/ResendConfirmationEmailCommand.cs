namespace Terminal.Backend.Application.Identity.ResendConfirmationEmail;

using MediatR;

public record ResendConfirmationEmailCommand(string Email) : IRequest;
