using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Aviv.Base.UI.Services.Authentication;

namespace Aviv.Base.UI.Services.Authentication;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;
    private readonly ILogger<CustomAuthStateProvider> _logger;

    public CustomAuthStateProvider(
        IAuthService authService,
        ILogger<CustomAuthStateProvider> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userInfo = await _authService.GetCurrentUserAsync();

            if (userInfo != null)
            {
                // User is authenticated, create claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userInfo.Username),
                    new Claim(ClaimTypes.Email, userInfo.Email)
                };

                // Add role claims
                foreach (var role in userInfo.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }

            // User is not authenticated
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error determining authentication state");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}