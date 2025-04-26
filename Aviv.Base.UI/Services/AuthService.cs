using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Aviv.Base.UI.Models.Authentication;

namespace Aviv.Base.UI.Services.Authentication;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginViewModel model);
    Task LogoutAsync();
    Task<UserInfo?> GetCurrentUserAsync();
    Task<bool> IsAuthenticatedAsync();
}

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;
    private readonly StateService _stateService;
    private readonly SessionService _sessionService;

    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthService> logger,
        StateService stateService,
        SessionService sessionService)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _stateService = stateService;
        _sessionService = sessionService;
    }

    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        try
        {
            // In a real application, you would validate credentials against a database
            // For this example, we'll use a hardcoded check
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return false;
            }

            // Mock authentication - replace with actual authentication logic
            // For example: bool isValid = await _userRepository.ValidateCredentialsAsync(model.Username, model.Password);
            bool isValidUser = IsValidUser(model.Username, model.Password);

            if (!isValidUser)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, "User"),
                // Add more claims as needed (e.g., user ID, email, etc.)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Configure remember me functionality
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe
                    ? DateTimeOffset.UtcNow.AddDays(30)
                    : DateTimeOffset.UtcNow.AddHours(2)
            };

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Store user info in session
            var userInfo = new UserInfo
            {
                Username = model.Username,
                FullName = "User " + model.Username, // Replace with actual user data
                Email = model.Username + "@example.com", // Replace with actual user data
                Roles = new List<string> { "User" }
            };

            // In a real application, store more user info
            await _sessionService.SetAppStateToSession(_stateService.GetAppState());

            _logger.LogInformation("User {Username} logged in at {Time}", model.Username, DateTime.UtcNow);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {Username}", model.Username);
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var username = httpContext.User.Identity?.Name;
                // Sign out
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Clear session
                _sessionService.DeleteAppStateFromSession();

                _logger.LogInformation("User {Username} logged out at {Time}", username, DateTime.UtcNow);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
        }
    }

    public async Task<UserInfo?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null || !httpContext.User.Identity!.IsAuthenticated)
        {
            return null;
        }

        var user = httpContext.User;
        var username = user.Identity.Name;

        // In a real application, you would likely fetch this information from a database
        // or user management service based on the authenticated user identity
        var userInfo = new UserInfo
        {
            Username = username!,
            FullName = "User " + username, // Replace with actual data
            Email = username + "@example.com", // Replace with actual data
            Roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
        };

        return await Task.FromResult(userInfo);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return await Task.FromResult(httpContext?.User.Identity?.IsAuthenticated ?? false);
    }

    // Mock method to validate users - replace with actual authentication
    private bool IsValidUser(string username, string password)
    {
        // For demo purposes only - in a real application, never store passwords in code
        // and always use password hashing
        var validCredentials = new Dictionary<string, string>
        {
            { "admin", "admin123" },
            { "user", "password" },
            { "test", "test123" }
        };

        return validCredentials.TryGetValue(username, out var storedPassword)
            && storedPassword == password;
    }
}