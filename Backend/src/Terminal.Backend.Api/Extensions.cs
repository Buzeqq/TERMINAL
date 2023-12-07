using System.Security.Claims;

public static class Extensions
{
    public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (Guid.TryParse(claimsPrincipal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                out var id))
        {
            return id;
        }

        return null;
    }
}