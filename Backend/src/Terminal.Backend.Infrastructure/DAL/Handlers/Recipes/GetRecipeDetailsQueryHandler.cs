using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Recipes.Get;
using Terminal.Backend.Core.Abstractions;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Recipes;

internal sealed class GetRecipeDetailsQueryHandler(
    TerminalDbContext dbContext,
    IParameterValueVisitor<GetSampleBaseParameterValueDto> parameterValueVisitor)
    : IRequestHandler<GetRecipeDetailsQuery, GetRecipeDetailsDto?>
{
    private readonly DbSet<Recipe> _recipes = dbContext.Recipes;

    public async Task<GetRecipeDetailsDto?> Handle(GetRecipeDetailsQuery request, CancellationToken cancellationToken)
    {
        var recipeData = await _recipes
            .TagWith("Get Recipe Details")
            .AsNoTracking()
            .Where(r => r.Id == request.Id)
            .Select(r => new
            {
                r.Id,
                r.Name,
                Steps = r.Steps.Select(s => new
                {
                    s.Id,
                    s.Comment,
                    Parameters = s.Values.Select(pv => new
                    {
                        ParameterValue = pv
                    })
                })
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (recipeData is null)
        {
            return null;
        }

        var recipe = new GetRecipeDetailsDto(
            recipeData.Id,
            recipeData.Name,
            recipeData.Steps.Select(s => new GetSampleStepsDto(
                s.Id,
                s.Parameters.Select(pv => pv.ParameterValue.Accept(parameterValueVisitor)),
                s.Comment)));

        return recipe;
    }
}
