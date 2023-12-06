using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipesAmountQueryHandler : IRequestHandler<GetRecipesAmountQuery, int>
{
    private readonly DbSet<Recipe> _recipes;

    public GetRecipesAmountQueryHandler(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<int> Handle(GetRecipesAmountQuery request, CancellationToken cancellationToken)
    {
        var amount = _recipes
            .AsNoTracking()
            .Count();

        return amount;
    }
}