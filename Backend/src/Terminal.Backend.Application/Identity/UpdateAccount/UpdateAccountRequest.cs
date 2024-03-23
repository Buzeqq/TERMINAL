namespace Terminal.Backend.Application.Identity.UpdateAccount;

public class UpdateAccountRequest
{
    public required string NewEmail { get; init; }
    public required string NewPassword { get; init; }
    public required string OldPassword { get; init; }
}
