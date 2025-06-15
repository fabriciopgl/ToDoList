using System.Security.Claims;

namespace TodoList.WebApi.Extensions;

public static class UserExtensions
{
    public static int GetCurrentUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID not found in token");
        }

        return int.Parse(userIdClaim);
    }

    public static string GetCurrentUserEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public static string GetCurrentUserName(this ClaimsPrincipal user)
    {
        return user.FindFirst("name")?.Value ?? string.Empty;
    }
}
