using MediatR;
using Microsoft.EntityFrameworkCore;
using Terminal.Backend.Application.DTO.Users;
using Terminal.Backend.Application.Queries.Users;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly DbSet<User> _users;

    public GetUserQueryHandler(TerminalDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken ct)
    {
        var user = (await _users
            .AsNoTracking()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Id.Equals(request.Id), ct))?.AsGetUserDto();

        return user;
    }
}