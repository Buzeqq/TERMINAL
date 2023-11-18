using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var result = await sender.Send(command, ct);

            return result;
        });

        app.MapGet("api/users/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetUserQuery(id);

            var response = await sender.Send(query, ct);

            return response is not null ? Results.Ok(response) : Results.NotFound();
        }).RequireAuthorization(Role.Registered);

        app.MapPost("api/users", async (
            RegisterUserCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            
        }).RequireAuthorization(Role.Administrator);
    }
}