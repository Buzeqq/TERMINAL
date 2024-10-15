using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Delete;

public record DeleteRecipeCommand(RecipeId Id) : IRequest;
