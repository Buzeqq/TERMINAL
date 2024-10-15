namespace Terminal.Backend.Api.Identity.Requests;

public record RegisterRequest(string Email, string Password, string RoleName);
