using MediatR;
using Terminal.Backend.Application.DTO.Recipes;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Recipes.Get;

public sealed class GetRecipesQuery : IRequest<GetRecipesDto>
{
    public GetRecipesQuery(int pageNumber, int pageSize, bool desc)
    {
        Parameters = new PagingParameters { PageSize = pageSize, PageNumber = pageNumber };
        OrderingParameters = new OrderingParameters { OrderBy = "RecipeName", Desc = desc };
    }

    public PagingParameters Parameters { get; set; }

    public OrderingParameters OrderingParameters { get; set; }
}