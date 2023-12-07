using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, GetRecipeDto?>
{
    private readonly DbSet<Recipe> _recipes;

    public GetRecipeQueryHandler(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<GetRecipeDto?> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var name = request.Name;
        var recipe = await _recipes.Include(r => r.Steps)
            .SingleOrDefaultAsync(r => r.RecipeName.Equals(name),
                cancellationToken);

        return recipe?.AsDto();
    }
}