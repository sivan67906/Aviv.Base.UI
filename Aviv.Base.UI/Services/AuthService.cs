using System.Text.Json;
using Aviv.Base.UI.Models.Authentication;
using Microsoft.JSInterop;

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
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<AuthService> _logger;
    private readonly StateService _stateService;

    public AuthService(
        IJSRuntime jsRuntime,
        ILogger<AuthService> logger,
        StateService stateService)
    {
        _jsRuntime = jsRuntime;
        _logger = logger;
        _stateService = stateService;
    }

    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        try
        {
            // For this demo, we'll use a simple validation
            // In a real application, this would verify credentials against a backend API
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return false;
            }

            bool isValidUser = IsValidUser(model.Username, model.Password);

            if (!isValidUser)
            {
                return false;
            }

            // Create auth token (in a real app, this would come from an API)
            string authToken = Guid.NewGuid().ToString();

            // Create user info to store
            UserInfo userInfo = new UserInfo
            {
                Username = model.Username,
                FullName = "User " + model.Username, // For demo purposes
                Email = model.Username + "@example.com", // For demo purposes
                Roles = ["User"]
            };

            // Store auth token and user info in local storage using the helper functions
            await _jsRuntime.InvokeVoidAsync("authLocalStorage.setAuthToken", authToken);
            await _jsRuntime.InvokeVoidAsync("authLocalStorage.setUserInfo", userInfo);

            // Store app state
            await _stateService.retrieveFromLocalStorage();

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
            // Get the current username for logging purposes
            UserInfo? userInfo = await GetCurrentUserAsync();
            string username = userInfo?.Username ?? "unknown";

            // Clear authentication data from local storage using the helper function
            await _jsRuntime.InvokeVoidAsync("authLocalStorage.clearAuth");

            _logger.LogInformation("User {Username} logged out at {Time}", username, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
        }
    }

    public async Task<UserInfo?> GetCurrentUserAsync()
    {
        try
        {
            // Get user info from local storage using the helper function
            string userInfoJson = await _jsRuntime.InvokeAsync<string>("authLocalStorage.getUserInfo");

            if (string.IsNullOrEmpty(userInfoJson))
            {
                return null;
            }

            // Deserialize user info
            return JsonSerializer.Deserialize<UserInfo>(userInfoJson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user");
            return null;
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            // Check if we have an auth token in local storage using the helper function
            return await _jsRuntime.InvokeAsync<bool>("authLocalStorage.isAuthenticated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking authentication status");
            return false;
        }
    }

    // Mock method to validate users - replace with actual authentication
    private bool IsValidUser(string username, string password)
    {
        // For demo purposes only - in a real application, never store passwords in code
        // and always use password hashing
        Dictionary<string, string> validCredentials = new Dictionary<string, string>
        {
            { "admin", "admin123" },
            { "user", "password" },
            { "test", "test123" }
        };

        return validCredentials.TryGetValue(username, out string? storedPassword)
            && storedPassword == password;
    }
}