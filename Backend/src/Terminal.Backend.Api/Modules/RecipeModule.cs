using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Commands.Recipe;
using Terminal.Backend.Application.Commands.Sample.Create;
using Terminal.Backend.Application.Queries.QueryParameters;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Application.Queries.Recipes.Search;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Modules;

public static class RecipeModule
{
    public static void UseRecipesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/recipes/search", async ([FromQuery] string searchPhrase, ISender sender, CancellationToken ct) =>
        {
            var query = new SearchRecipeQuery(searchPhrase);
            var recipes = await sender.Send(query, ct);

            return Results.Ok(recipes);
        }).RequireAuthorization(Permission.RecipeRead.ToString());

        app.MapGet("api/recipes/{name}", async (string name, ISender sender, CancellationToken ct) =>
        {
            var query = new GetRecipeQuery(name);

            var recipe = await sender.Send(query, ct);

            return recipe is null ? Results.NotFound() : Results.Ok(recipe);
        }).RequireAuthorization(Permission.RecipeRead.ToString());

        app.MapGet("api/recipes/{id:guid}/details", async (
            Guid id, 
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetRecipeDetailsQuery(id);

            var recipeDetails = await sender.Send(query, ct);

            return recipeDetails is null ? Results.NotFound() : Results.Ok(recipeDetails);
        }).RequireAuthorization(Permission.RecipeRead.ToString());
        
        app.MapGet("api/recipes", async (
            [FromQuery] int pageSize, 
            [FromQuery] int pageNumber,
            [FromQuery] bool? desc,
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new GetRecipesQuery(pageNumber, pageSize, desc ?? true);
            var recipes = await sender.Send(query, ct);
            return Results.Ok(recipes);
        }).RequireAuthorization(Permission.RecipeRead.ToString());

        app.MapPost("api/recipes", async(
            CreateRecipeCommand command,
            ISender sender,
            CancellationToken ct) =>
        {
            command = command with { Id = RecipeId.Create() };

            await sender.Send(command, ct);

            return Results.Created("api/recipes", new
            { 
                command.Id
            });
        }).RequireAuthorization(Permission.RecipeWrite.ToString());
        
        app.MapGet("api/recipes/amount", async (
            ISender sender, 
            CancellationToken ct) =>
        {
            var query = new GetRecipesAmountQuery();
            var amount = await sender.Send(query, ct);
            return Results.Ok(amount);
        }).RequireAuthorization(Permission.RecipeRead.ToString());
    }
}