using Terminal.Backend.Api.Common;
using Terminal.Backend.Api.Recipes.Requests;
using Terminal.Backend.Api.Swagger;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Recipes.Create;
using Terminal.Backend.Application.Recipes.Delete;
using Terminal.Backend.Application.Recipes.Get;
using Terminal.Backend.Application.Recipes.Update;
using Terminal.Backend.Core.Enums;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Api.Recipes;

public static class RecipeModule
{
    private const string ApiBaseRoute = "recipes";

    private static IEndpointRouteBuilder AddRecipesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{name}", async (string name, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetRecipeQuery(name);
                var recipe = await sender.Send(query, cancellationToken);
                return recipe is null ? Results.NotFound() : Results.Ok(recipe);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet( "/{id:guid}/details", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecipeDetailsQuery(id);
                var recipeDetails = await sender.Send(query, cancellationToken);
                return recipeDetails is null ? Results.NotFound() : Results.Ok(recipeDetails);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapGet("/", async (
                int pageSize,
                int pageIndex,
                string? searchPhrase,
                OrderDirection? orderDirection,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecipesQuery(
                    searchPhrase,
                    new PagingParameters(pageIndex, pageSize),
                    new OrderingParameters("Name", orderDirection));
                var recipes = await sender.Send(query, cancellationToken);
                return Results.Ok(recipes);
            }).RequireAuthorization(Permission.RecipeRead.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapPost("/", async (
                CreateRecipeRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var id = RecipeId.Create();
                await sender.Send(new CreateRecipeCommand(
                    id,
                    request.Name,
                    request.Steps.Select(s => new CreateSampleStepDto(
                            s.Values.SelectParameterValue<StepParameterValueDto>(
                                tm => new StepTextParameterValueDto(tm.ParameterId, tm.Value),
                                im => new StepIntegerParameterValueDto(im.ParameterId, im.Value),
                                dm => new StepDecimalParameterValueDto(dm.ParameterId, dm.Value)),
                            s.Comment))),
                    cancellationToken);
                return Results.Created(ApiBaseRoute, new { id });
            }).RequireAuthorization(Permission.RecipeWrite.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapDelete("/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteRecipeCommand(id), cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.RecipeDelete.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        app.MapPatch("/{id:guid}", async (
                Guid id,
                UpdateRecipeRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(new UpdateRecipeCommand(id, request.Name, request.Steps), cancellationToken);
                return Results.Ok();
            }).RequireAuthorization(Permission.RecipeUpdate.ToString())
            .WithTags(SwaggerSetup.RecipeTag);

        return app;
    }

    public static IEndpointRouteBuilder UseRecipesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(ApiBaseRoute).AddRecipesEndpoints();
        return app;
    }
}
