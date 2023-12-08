using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Recipe.Delete;

public sealed record DeleteRecipeCommand(RecipeId Id) : IRequest;