using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Commands.Recipe;
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
        app.MapGet(ApiBaseRoute + "/search",
                async ([FromQuery] string searchPhrase, ISender sender, CancellationToken ct) =>
                {
                    var query = new SearchRecipeQuery(searchPhrase);
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

        app.MapPost(ApiBaseRoute, async(
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
    }
}