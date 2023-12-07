using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUsersAmountQueryHandler : IRequestHandler<GetUsersAmountQuery, int>
{
    private readonly DbSet<User> _users;

    public GetUsersAmountQueryHandler(TerminalDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<int> Handle(GetUsersAmountQuery request, CancellationToken cancellationToken)
    {
        var amount = _users
            .AsNoTracking()
            .Count();

        return amount;
    }
}