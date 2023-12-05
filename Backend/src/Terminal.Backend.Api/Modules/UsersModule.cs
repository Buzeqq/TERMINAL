using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Users.Create;
using Terminal.Backend.Application.Commands.Users.Invitations;
using Terminal.Backend.Application.Commands.Users.Login;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Application.Queries.Users.Invitations;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Api.Modules;

public static class UsersModule
{
    private const string ApiRouteBase = "api/users";
    public static void UseUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRouteBase + "/login", async (
            [FromBody] LoginCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var token = await sender.Send(command, ct);

            return token;
        }).AllowAnonymous();

        app.MapGet(ApiRouteBase + "/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetUserQuery(id);

            var user = await sender.Send(query, ct);

            return user is not null ? Results.Ok(user) : Results.NotFound();
        }).RequireAuthorization(Role.Registered);
        
        app.MapGet(ApiRouteBase, async (
            [FromQuery] int pageNumber, 
            [FromQuery] int pageSize, 
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetUsersQuery(pageNumber, pageSize, orderBy ?? "Role", desc ?? true);
            var users = await sender.Send(query, ct);
            return Results.Ok(users);
        }).RequireAuthorization(Role.Moderator);

        app.MapPost(ApiRouteBase, async (
            [FromBody] CreateUserCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var invitation = await sender.Send(command, ct);

            return invitation;
        }).RequireAuthorization(Role.Administrator);

        app.MapGet(ApiRouteBase + "/invitations/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new CheckInvitationQuery(id);

            var invitation = await sender.Send(query, ct);

            return invitation is null ? Results.BadRequest() : Results.Ok(invitation);
        }).AllowAnonymous();

        app.MapPost(ApiRouteBase + "/invitations/accept/{id:guid}", async (
            Guid id,
            [FromBody] AcceptInvitationCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            command = command with { Id = id };

            await sender.Send(command, ct);

            return Results.Created(ApiRouteBase, null);
        }).AllowAnonymous();
        
        app.MapGet(ApiRouteBase + "/amount", async (
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new GetUsersAmountQuery();
            var amount = await sender.Send(query, ct);
            return Results.Ok(amount);
        }).RequireAuthorization(Role.Moderator);
    }
}