using MediatR;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Recipe;

public record CreateRecipeCommand(RecipeId Id, string Name, IEnumerable<CreateSampleStepDto> Steps) : IRequest;