using System.Security.Claims;

namespace CondoHub.API.Extensions;

/// <summary>Extracts strongly typed identifiers from the authenticated principal's claims.</summary>
public static class ClaimsPrincipalExtensions
{
    private const string CondominiumIdClaimType = "CondominiumId";

    public static bool TryGetUserId(this ClaimsPrincipal user, out long userId)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub");
        return long.TryParse(userIdClaim, out userId) && userId > 0;
    }

    public static bool TryGetCondominiumId(this ClaimsPrincipal user, out long condominiumId)
    {
        var condominiumIdClaim = user.FindFirstValue(CondominiumIdClaimType);
        return long.TryParse(condominiumIdClaim, out condominiumId) && condominiumId > 0;
    }
}
