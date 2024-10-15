using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipeQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetRecipeQuery, GetRecipeDto?>
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public async Task<GetRecipeDto?> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var name = request.Name;
        var recipe = await _recipes
            .TagWith("Get Recipe by name")
            .Where(r => r.Name == name)
            .Select(r => new GetRecipeDto(r.Id, r.Name))
            .SingleOrDefaultAsync(cancellationToken);

        return recipe;
    }
}
