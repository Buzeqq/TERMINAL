using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Commands.Recipe.Create;
using Terminal.Backend.Application.Commands.Recipe.Delete;
using Terminal.Backend.Application.Commands.Recipe.Update;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Application.Queries.Recipes.Search;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class RecipeModule
{
    private const string ApiBaseRoute = "api/recipes";

    public static void UseRecipesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiBaseRoute + "/search", async (
                [FromQuery] string searchPhrase,
                [FromQuery] int pageSize,
                [FromQuery] int pageNumber,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new SearchRecipeQuery(searchPhrase, pageNumber, pageSize);
                var recipes = await sender.Send(query, ct);
                return Results.Ok(recipes);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet(ApiBaseRoute + "/{name}", async (string name, ISender sender, CancellationToken ct) =>
            {
                var query = new GetRecipeQuery(name);
                var recipe = await sender.Send(query, ct);
                return recipe is null ? Results.NotFound() : Results.Ok(recipe);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet(ApiBaseRoute + "/{id:guid}/details", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetRecipeDetailsQuery(id);
                var recipeDetails = await sender.Send(query, ct);
                return recipeDetails is null ? Results.NotFound() : Results.Ok(recipeDetails);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet(ApiBaseRoute, async (
                [FromQuery] int pageSize,
                [FromQuery] int pageNumber,
                [FromQuery] bool? desc,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetRecipesQuery(pageNumber, pageSize, desc ?? true);
                var recipes = await sender.Send(query, ct);
                return Results.Ok(recipes);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapPost(ApiBaseRoute, async (
                CreateRecipeCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = RecipeId.Create() };
                await sender.Send(command, ct);
                return Results.Created(ApiBaseRoute, new { command.Id });
            }).RequireAuthorization(Permission.RecipeWrite.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet(ApiBaseRoute + "/amount", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetRecipesAmountQuery();
                var amount = await sender.Send(query, ct);
                return Results.Ok(amount);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapDelete(ApiBaseRoute + "/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken ct) =>
            {
                await sender.Send(new DeleteRecipeCommand(id), ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.RecipeDelete.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapPatch(ApiBaseRoute + "/{id:guid}", async (
                Guid id,
                UpdateRecipeCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with { Id = id };
                await sender.Send(command, ct);
                return Results.Ok();
            }).RequireAuthorization(Permission.RecipeUpdate.ToString())
            .WithTags(SwaggerSetup.RecipeTag);
    }
}