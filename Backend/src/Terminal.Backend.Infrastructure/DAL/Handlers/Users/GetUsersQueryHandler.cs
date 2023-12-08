using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Users;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDto>
{
    private readonly DbSet<User> _users;

    public GetUsersQueryHandler(TerminalDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken ct)
    {
        var users = await _users
            .AsNoTracking()
            .OrderBy(request.OrderingParameters)
            .Paginate(request.Parameters)
            .Select(u => new GetUsersDto.UserDto(u.Id.Value, u.Email.Value, u.Role.Name))
            .ToListAsync(ct);

        return new GetUsersDto { Users = users };
    }
}