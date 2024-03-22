namespace Terminal.Backend.Application.Identity.ResetPassword;

public class ResetPasswordRequest
{
    public required string Email { get; init; }
    public required string NewPassword { get; init; }
    public required string Code { get; init; }
}
