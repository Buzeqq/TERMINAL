using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Infrastructure.Authentication;

internal interface IAuthorizationService
{
    Task<HashSet<string>> GetPermissionsAsync(UserId userId);
    Task<Role> GetRole(UserId userId);
}