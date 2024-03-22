namespace Terminal.Backend.Application.Identity.ForgotPassword;

using Core.ValueObjects;
using MediatR;

public record ForgotPasswordCommand(Email Email) : IRequest;
