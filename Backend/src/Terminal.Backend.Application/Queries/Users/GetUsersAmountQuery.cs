using MediatR;

namespace Terminal.Backend.Application.Queries.Users;

public sealed class GetUsersAmountQuery : IRequest<int>
{
    public int Amount { get; set; }
}