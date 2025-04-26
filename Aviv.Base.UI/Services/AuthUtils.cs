using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Aviv.Base.UI.Services.Authentication;

public static class AuthUtils
{
    /// <summary>
    /// Gets the current username from authentication state
    /// </summary>
    public static async Task<string?> GetCurrentUsernameAsync(AuthenticationStateProvider authStateProvider)
    {
        AuthenticationState authState = await authStateProvider.GetAuthenticationStateAsync();
        ClaimsPrincipal user = authState.User;

        return user.Identity?.IsAuthenticated ?? false ? user.Identity.Name : null;
    }

    /// <summary>
    /// Checks if the current user is in the specified role
    /// </summary>
    public static async Task<bool> IsUserInRoleAsync(AuthenticationStateProvider authStateProvider, string role)
    {
        AuthenticationState authState = await authStateProvider.GetAuthenticationStateAsync();
        return authState.User.IsInRole(role);
    }

    /// <summary>
    /// Gets all roles for the current user
    /// </summary>
    public static async Task<IEnumerable<string>> GetUserRolesAsync(AuthenticationStateProvider authStateProvider)
    {
        AuthenticationState authState = await authStateProvider.GetAuthenticationStateAsync();
        return authState.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);
    }

    /// <summary>
    /// Extracts return URL from query string if present
    /// </summary>
    public static string? ExtractReturnUrl(string currentUrl)
    {
        if (string.IsNullOrEmpty(currentUrl) || !currentUrl.Contains("returnUrl="))
        {
            return null;
        }

        try
        {
            Uri uri = new Uri(currentUrl);
            string? returnUrl = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("returnUrl");

            // Validate return URL (only allow relative URLs for security)
            if (!string.IsNullOrEmpty(returnUrl) && !Uri.IsWellFormedUriString(returnUrl, UriKind.Absolute))
            {
                return Uri.UnescapeDataString(returnUrl);
            }
        }
        catch
        {
            // Ignore parsing errors
        }

        return null;
    }
}