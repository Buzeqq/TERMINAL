namespace Terminal.Backend.Application.Identity.ResetPassword;

using Core.ValueObjects;

public record ResetPasswordCommand(Email Email, Password NewPassword, string Code) : IRequest;
