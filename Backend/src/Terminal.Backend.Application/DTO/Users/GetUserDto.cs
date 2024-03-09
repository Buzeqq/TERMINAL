namespace Terminal.Backend.Application.DTO.Users;

public class GetUserDto
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}