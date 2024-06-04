using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Identity.ConfirmEmail;
using Terminal.Backend.Application.Identity.Login;
using Terminal.Backend.Application.Identity.Logout;
using Terminal.Backend.Application.Identity.Refresh;
using Terminal.Backend.Application.Identity.Register;
using Terminal.Backend.Application.Identity.ForgotPassword;
using Terminal.Backend.Application.Identity.ResendConfirmationEmail;
using Terminal.Backend.Application.Identity.ResetPassword;
using Terminal.Backend.Application.Identity.UpdateAccount;
using Terminal.Backend.Application.Identity.GetUserInfo;

namespace Terminal.Backend.Api.Identity;

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
                var request = new ConfirmEmailRequest { UserId = userId, Code = code, ChangedEmail = changedEmail };
                var command = request.Adapt<ConfirmEmailCommand>();
                await sender.Send(command, cancellationToken);
                return Results.Ok("Thank you for confirming your email.");
            }).WithName("Email confirmation endpoint")
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/resendConfirmationEmail", async (
                [FromBody] ResendConfirmationEmailRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<ResendConfirmationEmailCommand>();
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            })
            .WithTags(SwaggerSetup.IdentityTag);

        app.MapPost("/forgotPassword", async (
                [FromBody] ForgotPasswordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<ForgotPasswordCommand>();
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            })
            .WithTags(SwaggerSetup.IdentityTag);

        app.MapPost("/resetPassword", async (
                [FromBody] ResetPasswordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<ResetPasswordCommand>();
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            })
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapGet("/2fa", () => { })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/account/2fa", () => { })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapGet("/account/info", async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var userInfo = await sender.Send(new GetUserInfoQuery(), cancellationToken);
                return Results.Ok(userInfo);
            })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/account/info", async (
                [FromBody] UpdateAccountRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<UpdateAccountCommand>();
                await sender.Send(command, cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);
    }

    public static void UseIdentityEndpoints(this IEndpointRouteBuilder app) =>
        app.MapGroup(ApiBaseRoute)
            .MapIdentityEndpoints();
}
