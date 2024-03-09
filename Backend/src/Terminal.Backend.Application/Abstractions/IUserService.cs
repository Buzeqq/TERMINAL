namespace Terminal.Backend.Application.Abstractions;

public interface IUserService
{
    Task RegisterAsync(string email, string password);
}