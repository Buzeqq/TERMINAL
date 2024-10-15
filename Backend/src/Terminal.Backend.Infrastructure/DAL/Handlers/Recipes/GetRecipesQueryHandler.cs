using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Recipes.Get;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipesQueryHandler(TerminalDbContext dbContext)
    : IRequestHandler<GetRecipesQuery, GetRecipesDto>
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public async Task<GetRecipesDto> Handle(GetRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var (searchPhrase, pagingParameters, orderingParameters) = request;

        var query = _recipes
            .TagWith($"Get recipes ordered [{orderingParameters}] and paginated [{pagingParameters}]")
            .AsNoTracking();

        if (searchPhrase is not null)
        {
            query = query
                .TagWith($"Get recipes searching [{searchPhrase}]")
                .Where(r => r.Name.Value.Contains(searchPhrase));
        }


        var totalCount = await query.CountAsync(cancellationToken);

        var recipes = await query
            .OrderBy(orderingParameters)
            .Paginate(pagingParameters)
            .ToListAsync(cancellationToken);

        return GetRecipesDto.Create(recipes, totalCount, pagingParameters);
    }
}
