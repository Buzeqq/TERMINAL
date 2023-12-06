using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Commands.Users.Create;
using Terminal.Backend.Application.Commands.Users.Invitations;
using Terminal.Backend.Application.Commands.Users.Login;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Application.Queries.Users.Invitations;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Api.Modules;

public static class UsersModule
{
    private const string ApiBaseRoute = "api/users";
    public static void UseUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiBaseRoute + "/login", async (
            [FromBody] LoginCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var token = await sender.Send(command, ct);
            return token;
        }).AllowAnonymous()
            .WithTags(SwaggerSetup.UserTag);

        app.MapGet(ApiBaseRoute + "/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetUserQuery(id);
            var user = await sender.Send(query, ct);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        }).RequireAuthorization(Role.Registered)
            .WithTags(SwaggerSetup.UserTag);
        
        app.MapGet(ApiBaseRoute, async (
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
        }).RequireAuthorization(Role.Moderator)
            .WithTags(SwaggerSetup.UserTag);

        app.MapPost(ApiBaseRoute, async (
            [FromBody] CreateUserCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            var invitation = await sender.Send(command, ct);
            return Results.Created($"{ApiBaseRoute}/invitations", invitation);
        }).RequireAuthorization(Role.Administrator)
            .WithTags(SwaggerSetup.UserTag);

        app.MapGet(ApiBaseRoute + "/invitations/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new CheckInvitationQuery(id);
            var invitation = await sender.Send(query, ct);
            return invitation is null ? Results.BadRequest() : Results.Ok(invitation);
        }).AllowAnonymous()
            .WithTags(SwaggerSetup.UserTag);

        app.MapPost(ApiBaseRoute + "/invitations/accept/{id:guid}", async (
            Guid id,
            [FromBody] AcceptInvitationCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            command = command with { Id = id };
            await sender.Send(command, ct);
            return Results.Created(ApiBaseRoute, null);
        }).AllowAnonymous()
            .WithTags(SwaggerSetup.UserTag);
        
        app.MapGet(ApiBaseRoute + "/amount", async (
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new GetUsersAmountQuery();
            var amount = await sender.Send(query, ct);
            return Results.Ok(amount);
        }).RequireAuthorization(Role.Moderator)
            .WithTags(SwaggerSetup.UserTag);
    }
}