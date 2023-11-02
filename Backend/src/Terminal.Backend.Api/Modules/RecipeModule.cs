using MediatR;
using Microsoft.AspNetCore.Mvc;
using Terminal.Backend.Application.Queries;

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
        });
    }
}