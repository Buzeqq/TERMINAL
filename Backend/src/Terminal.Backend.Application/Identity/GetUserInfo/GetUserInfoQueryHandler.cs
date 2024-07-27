using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Terminal.Backend.Application.Common;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Application.Identity.GetUserInfo;

using DTO.Users;

public class GetUserInfoQueryHandler(
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager) : IRequestHandler<GetUserInfoQuery, UserInfo>
{
    public async Task<UserInfo> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var cp = httpContextAccessor.HttpContext?.User ?? throw new UserNotFoundException();
        var user = await userManager.GetUserAsync(cp);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var roles = await userManager.GetRolesAsync(user);
        return new UserInfo()
        {
            Id = user.Id,
            Email = user.Email!,
            IsEmailConfirmed = user.EmailConfirmed,
            Roles = roles
        };
    }
}
