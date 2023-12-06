using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.Recipes.Search;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class SearchRecipeQueryHandler : IRequestHandler<SearchRecipeQuery, GetRecipesDto>
{
    private readonly DbSet<Recipe> _recipes;

    public SearchRecipeQueryHandler(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<GetRecipesDto> Handle(SearchRecipeQuery request, CancellationToken ct) =>
        new()
        {
            Recipes = await _recipes
                .AsNoTracking()
                .Where(r => EF.Functions.ILike(r.RecipeName, $"%{request.SearchPhrase}%"))
                .Select(r => new GetRecipesDto.RecipeDto(r.Id, r.RecipeName))
                .Paginate(request.Parameters)
                .ToListAsync(ct)
        };
}