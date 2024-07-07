using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipesAmountQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetRecipesAmountQuery, int>
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public Task<int> Handle(GetRecipesAmountQuery request, CancellationToken cancellationToken) =>
        _recipes
            .AsNoTracking()
            .CountAsync(cancellationToken);
}