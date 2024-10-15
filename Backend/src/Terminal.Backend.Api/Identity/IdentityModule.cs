using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Identity.Requests;
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
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Identity;

internal static class IdentityEndpointsModule
{
    private const string ApiBaseRoute = "identity";

    private static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (
                [FromBody] RegisterRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new RegisterCommand(request.Email, request.Password, request.RoleName), cancellationToken);
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
                [FromQuery] Guid userId,
                [FromQuery] string code,
                [FromQuery] string? changedEmail,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var request = new ConfirmEmailRequest(userId, code, changedEmail);

                await sender.Send(
                    new ConfirmEmailCommand(
                        request.UserId,
                        request.Code,
                        request.ChangedEmail is not null ? new Email(request.ChangedEmail) : null),
                    cancellationToken);

                return Results.Ok("Thank you for confirming your email.");
            }).WithName("Email confirmation endpoint")
            .WithTags(SwaggerSetup.IdentityTag);


        app.MapPost("/resendConfirmationEmail", async (
                [FromBody] ResendConfirmationEmailRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new ResendConfirmationEmailCommand(request.Email), cancellationToken);

                return Results.Ok();
            })
            .WithTags(SwaggerSetup.IdentityTag);

        app.MapPost("/forgotPassword", async (
                [FromBody] ForgotPasswordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new ForgotPasswordCommand(request.Email), cancellationToken);

                return Results.Ok();
            })
            .WithTags(SwaggerSetup.IdentityTag);

        app.MapPost("/resetPassword", async (
                [FromBody] ResetPasswordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(
                    new ResetPasswordCommand(
                        request.Email,
                        request.NewPassword,
                        request.Code),
                    cancellationToken);

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
                await sender.Send(new UpdateAccountCommand(
                    request.NewEmail is not null ? new Email(request.NewEmail) : null,
                    request.NewPassword is not null ? new Password(request.NewPassword) : null,
                    request.OldPassword is not null ? new Password(request.OldPassword) : null),
                    cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .WithTags(SwaggerSetup.IdentityTag);

        return app;
    }

    public static IEndpointRouteBuilder UseIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute)
            .MapIdentityEndpoints();
        return app;
    }
}
