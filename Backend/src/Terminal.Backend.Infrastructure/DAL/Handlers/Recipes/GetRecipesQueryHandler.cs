using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, GetRecipesDto>
{
    private readonly DbSet<Recipe> _recipes;

    public GetRecipesQueryHandler(TerminalDbContext dbContext)
    {
        _recipes = dbContext.Recipes;
    }

    public async Task<GetRecipesDto> Handle(GetRecipesQuery request, CancellationToken cancellationToken) =>
        new()
        {
            Recipes = await _recipes
                .Paginate(request.Parameters)
                .Select(r => r.AsDto())
                .ToListAsync(cancellationToken)
        };
}