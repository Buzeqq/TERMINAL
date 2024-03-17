using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Terminal.Backend.Application.Abstractions;

public interface IUserService
{
    Task RegisterAsync(string email, string password);

    Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult>> SignInAsync(
        string email,
        string password,
        string? twoFactorCode,
        string? twoFactorRecoveryCode,
        bool useCookies = true,
        bool useSessionCookies = true);
}