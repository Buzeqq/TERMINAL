namespace Terminal.Backend.Application.Identity.UpdateAccount;

public class UpdateAccountRequest
{
    public string? NewEmail { get; init; }
    public string? NewPassword { get; init; }
    public string? OldPassword { get; init; }
}
