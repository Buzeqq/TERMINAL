using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Recipes.Delete;

public sealed record DeleteRecipeCommand(RecipeId Id) : IRequest;