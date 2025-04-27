using System.Security.Claims;
using System.Text.Json;
using Aviv.Base.UI.Models.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Aviv.Base.UI.Services.Authentication;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;
    private readonly ILogger<CustomAuthStateProvider> _logger;
    private readonly IJSRuntime _jsRuntime;

    public CustomAuthStateProvider(
        IAuthService authService,
        ILogger<CustomAuthStateProvider> logger,
        IJSRuntime jsRuntime)
    {
        _authService = authService;
        _logger = logger;
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Check if we have user info in local storage using the helper function
            string userInfoJson = await _jsRuntime.InvokeAsync<string>("authLocalStorage.getUserInfo");

            if (string.IsNullOrEmpty(userInfoJson))
            {
                // User is not authenticated
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Deserialize user info
            UserInfo? userInfo = JsonSerializer.Deserialize<UserInfo>(userInfoJson);

            if (userInfo == null)
            {
                // User info is invalid
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Create claims based on user info
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim(ClaimTypes.Email, userInfo.Email)
            ];

            // Add role claims
            foreach (string role in userInfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create claims identity and principal
            ClaimsIdentity identity = new ClaimsIdentity(claims, "LocalStorageAuth");
            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error determining authentication state: {Message}", ex.Message);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}