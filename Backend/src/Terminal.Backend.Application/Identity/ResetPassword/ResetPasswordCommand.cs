namespace Terminal.Backend.Application.Identity.ResetPassword;

using Core.ValueObjects;
using MediatR;

public record ResetPasswordCommand(Email Email, Password NewPassword, string Code) : IRequest;
