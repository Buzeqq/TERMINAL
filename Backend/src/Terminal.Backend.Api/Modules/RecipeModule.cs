using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Queries.Recipes;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Application.Queries.Recipes.Search;
using Terminal.Backend.Core.Enums;

namespace Terminal.Backend.Api.Modules;

public static class RecipeModule
{
    public static void UseRecipesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/recipe/search", async ([FromQuery] string searchPhrase, ISender sender, CancellationToken ct) =>
        {
            var query = new SearchRecipeQuery(searchPhrase);
            var recipes = await sender.Send(query, ct);

            return Results.Ok(recipes);
        }).RequireAuthorization(Permission.RecipeRead.ToString());

        app.MapGet("api/recipe/{name}", async (string name, ISender sender, CancellationToken ct) =>
        {
            var query = new GetRecipeQuery(name);

            var recipe = await sender.Send(query, ct);

            return recipe is null ? Results.NotFound() : Results.Ok(recipe);
        }).RequireAuthorization(Permission.RecipeRead.ToString());

        app.MapGet("api/recipe/{id:guid}/details", async (
            Guid id, 
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetRecipeDetailsQuery(id);

            var recipeDetails = await sender.Send(query, ct);

            return recipeDetails is null ? Results.NotFound() : Results.Ok(recipeDetails);
        }).RequireAuthorization(Permission.RecipeRead.ToString());
    }
}