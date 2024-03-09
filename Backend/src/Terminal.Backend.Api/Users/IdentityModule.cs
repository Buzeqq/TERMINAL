using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Users.Login;
using Terminal.Backend.Application.Users.Register;

namespace Terminal.Backend.Api.Users;

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
        });
        
        app.MapPost("/login", async (
            [FromBody] LoginRequest loginRequest,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = loginRequest.Adapt<LoginCommand>();
            var result = await sender.Send(command, cancellationToken);
        });
        
        app.MapGet("/logout", () =>
        {
            
        });
        
        app.MapPost("/refresh", () =>
        {
            
        });
        
        app.MapGet("/confirm-email", () =>
        {
            
        }).WithName("Email confirmation endpoint");
        
        app.MapPost("/resendConfirmationEmail", () =>
        {
            
        });
        
        app.MapPost("/resetPassword", () =>
        {
            
        });
        
        app.MapGet("/2fa", () =>
        {
            
        });
        
        app.MapPost("/account/2fa", () =>
        {
            
        });
        
        app.MapGet("/account/info", () =>
        {
            
        });
        
        app.MapPost("/account/info", () =>
        {
            
        });
    }

    public static void UseIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute)
            .MapIdentityEndpoints();
    }
}