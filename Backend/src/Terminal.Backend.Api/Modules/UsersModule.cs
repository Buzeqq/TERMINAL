using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Users.Create;
using Terminal.Backend.Application.Commands.Users.Login;
using Terminal.Backend.Application.Commands.Users.Register;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Api.Modules;

public static class UsersModule
{
    public static void UseUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/login", async (
            [FromBody] LoginCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var token = await sender.Send(command, ct);

            return token;
        }).AllowAnonymous();

        app.MapGet("api/users/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetUserQuery(id);

            var user = await sender.Send(query, ct);

            return user is not null ? Results.Ok(user) : Results.NotFound();
        }).RequireAuthorization(Role.Registered);

        app.MapPost("api/users", async (
            [FromBody] CreateUserCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var invitation = await sender.Send(command, ct);
        }).RequireAuthorization(Role.Administrator);
    }
}