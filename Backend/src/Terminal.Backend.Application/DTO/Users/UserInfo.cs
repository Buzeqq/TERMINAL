namespace Terminal.Backend.Application.DTO.Users;

public class UserInfo
{
    public string Id { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required bool IsEmailConfirmed { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
}
