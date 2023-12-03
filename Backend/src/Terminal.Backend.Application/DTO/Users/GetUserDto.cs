namespace Terminal.Backend.Application.DTO.Users;

public class GetUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}