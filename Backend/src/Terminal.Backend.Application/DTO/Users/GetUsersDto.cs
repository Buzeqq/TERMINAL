namespace Terminal.Backend.Application.DTO.Users;

public class GetUsersDto
{
    public IEnumerable<UserDto> Users { get; set; } = [];

    public sealed record UserDto(Guid Id, string Email, string Role);
}