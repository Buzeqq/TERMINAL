using MediatR;

namespace Terminal.Backend.Application.Queries.Recipes.Get;

public sealed class GetRecipesAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}