using MediatR;

namespace Terminal.Backend.Application.Recipes.Get;

public sealed class GetRecipesAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}