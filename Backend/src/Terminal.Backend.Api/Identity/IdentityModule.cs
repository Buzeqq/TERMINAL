using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Identity.ConfirmEmail;
using Terminal.Backend.Application.Identity.Login;
using Terminal.Backend.Application.Identity.Logout;
using Terminal.Backend.Application.Identity.Refresh;
using Terminal.Backend.Application.Identity.Register;

namespace Terminal.Backend.Api.Identity;

using Application.Identity.ResendConfirmationEmail;
using Core.ValueObjects;
using LoginRequest = LoginRequest;
using RefreshRequest = RefreshRequest;
using RegisterRequest = RegisterRequest;
using ResendConfirmationEmailRequest = Application.Identity.ResendConfirmationEmail.ResendConfirmationEmailRequest;

internal static class IdentityEndpointsModule
{
    private const string ApiBaseRoute = "api/identity";

    private static void MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (
                [FromBody] RegisterRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<RegisterCommand>();
                await sender.Send(command, cancellationToken);
            })
            .WithTags(SwaggerSetup.IdentityTag);

        app.MapPost("/login", async (
                [FromBody] LoginRequest loginRequest,
                [FromQuery] bool useCookies,
                [FromQuery] bool useSessionCookies,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = loginRequest.Adapt<LoginCommand>();
                command = command with { UseCookies = useCookies, UseSessionCookies = useSessionCookies };
                await sender.Send(command, cancellationToken);
            })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/logout", async (
                [FromBody] LogoutRequest logoutRequest,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new LogoutCommand(), cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/refresh", async (
                [FromBody] RefreshRequest refreshRequest,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = refreshRequest.Adapt<RefreshCommand>();
                await sender.Send(command, cancellationToken);
            })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapGet("/confirm-email", async (
                [FromQuery] string userId,
                [FromQuery] string code,
                [FromQuery] string? changedEmail,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                Email? email = null;
                if (changedEmail is not null)
                {
                    email = changedEmail;
                }

                var command = new ConfirmEmailCommand(userId, code, email);
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            }).WithName("Email confirmation endpoint")
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/resendConfirmationEmail", async (
                [FromBody] ResendConfirmationEmailRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<ResendConfirmationEmailCommand>();
                await sender.Send(command, cancellationToken);
            })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/resetPassword", () => { })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapGet("/2fa", () => { })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/account/2fa", () => { })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapGet("/account/info", () => { })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/account/info", () => { })
            .WithTags(SwaggerSetup.IdentityTag);
    }

    public static void UseIdentityEndpoints(this IEndpointRouteBuilder app) =>
        app.MapGroup(ApiBaseRoute)
            .MapIdentityEndpoints();
}
