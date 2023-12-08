using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipeDetailsQueryHandler : IRequestHandler<GetRecipeDetailsQuery, GetRecipeDetailsDto?>
{
    private readonly DbSet<Recipe> _recipes;

    public GetRecipeDetailsQueryHandler(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<GetRecipeDetailsDto?> Handle(GetRecipeDetailsQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _recipes.AsNoTracking()
            .Include(r => r.Steps)
            .ThenInclude(s => s.Parameters)
            .ThenInclude(s => s.Parameter)
            .SingleOrDefaultAsync(r => r.Id.Equals(request.Id), cancellationToken);

        return recipe is null
            ? null
            : new GetRecipeDetailsDto(recipe.Id, recipe.RecipeName.Name, recipe.Steps.AsStepsDto());
    }
}