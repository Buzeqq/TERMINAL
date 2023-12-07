using MediatR;
using Terminal.Backend.Application.DTO.Users;
using Terminal.Backend.Application.Queries.QueryParameters;

namespace Terminal.Backend.Application.Queries.Users;

public class GetUsersQuery : IRequest<GetUsersDto>
{
    public PagingParameters Parameters { get; set; }

    public OrderingParameters OrderingParameters { get; set; }

    public GetUsersQuery(int pageNumber, int pageSize, string orderBy, bool desc)
    {
        Parameters = new PagingParameters { PageNumber = pageNumber, PageSize = pageSize };
        OrderingParameters = new OrderingParameters { OrderBy = orderBy, Desc = desc };
    }
}