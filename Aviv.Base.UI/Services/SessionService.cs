using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

public class SessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SessionService> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly JsonSerializerOptions _jsonOptions;

    private const string AppStateKey = "AppState";
    private const string InitialAppStateKey = "InitialAppState";

    // Constructor with proper dependency injection
    public SessionService(
        IHttpContextAccessor httpContextAccessor,
        ILogger<SessionService> logger,
        IMemoryCache memoryCache)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        // Configure JSON serialization options
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };
    }

    public async Task SetAppStateToSession(AppState state)
    {
        if (state == null)
        {
            _logger.LogWarning("Attempted to set null AppState to session");
            return;
        }

        try
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                // Check if session is available
                if (httpContext.Session == null)
                {
                    _logger.LogWarning("Session is not available, storing AppState in memory cache only");
                    // Store in memory cache only
                    _memoryCache.Set($"AppState_{httpContext.Connection.Id}", state,
                        new MemoryCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromMinutes(30),
                            Priority = CacheItemPriority.High
                        });
                }
                else
                {
                    // Store in session and memory cache
                    string jsonState = JsonSerializer.Serialize(state, _jsonOptions);
                    httpContext.Session.SetString(AppStateKey, jsonState);

                    // Also cache in memory for faster access
                    _memoryCache.Set($"AppState_{httpContext.Connection.Id}", state,
                        new MemoryCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromMinutes(30),
                            Priority = CacheItemPriority.High
                        });

                    _logger.LogDebug("Successfully set AppState to session");
                }
            }
            else
            {
                _logger.LogWarning("HttpContext was null when setting AppState");
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting AppState to session");
        }
    }

    public void DeleteAppStateFromSession()
    {
        try
        {
            HttpContext? httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext != null)
            {
                // Remove from memory cache
                _memoryCache.Remove($"AppState_{httpContext.Connection.Id}");

                // Try to remove from session if available
                try
                {
                    if (httpContext.Session != null)
                    {
                        httpContext.Session.Remove(AppStateKey);
                    }
                }
                catch (InvalidOperationException)
                {
                    _logger.LogWarning("Session not available when trying to delete AppState");
                }

                _logger.LogDebug("Successfully deleted AppState from storage");
            }
            else
            {
                _logger.LogWarning("HttpContext was null when deleting AppState");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AppState from storage");
        }
    }

    public AppState GetAppStateFromSession()
    {
        try
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            // Check if HttpContext is null
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext was null when getting AppState");
                return new AppState(); // Return default state
            }

            // Try to get from memory cache first for better performance
            string connectionId = httpContext.Connection.Id;
            if (_memoryCache.TryGetValue($"AppState_{connectionId}", out AppState? cachedState) && cachedState != null)
            {
                return cachedState;
            }

            // Try to get from session if available
            try
            {
                if (httpContext.Session != null)
                {
                    string? jsonState = httpContext.Session.GetString(AppStateKey);
                    if (!string.IsNullOrEmpty(jsonState))
                    {
                        AppState? appState = JsonSerializer.Deserialize<AppState>(jsonState, _jsonOptions);
                        if (appState != null)
                        {
                            // Cache the result for faster access next time
                            _memoryCache.Set($"AppState_{connectionId}", appState,
                                new MemoryCacheEntryOptions
                                {
                                    SlidingExpiration = TimeSpan.FromMinutes(30),
                                    Priority = CacheItemPriority.High
                                });
                            return appState;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                _logger.LogWarning("Session not configured or not available when getting AppState");
                // Continue to return default state
            }

            // Return default state if session data is not available
            return new AppState();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AppState from storage");
            return new AppState(); // Return default state in case of error
        }
    }

    public async Task SetInitalAppStateToSession(AppState state)
    {
        if (state == null)
        {
            _logger.LogWarning("Attempted to set null InitialAppState to session");
            return;
        }

        try
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                // Store in memory cache
                _memoryCache.Set($"InitialAppState_{httpContext.Connection.Id}", state,
                    new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(30)
                    });

                // Try to store in session if available
                try
                {
                    if (httpContext.Session != null)
                    {
                        string jsonState = JsonSerializer.Serialize(state, _jsonOptions);
                        httpContext.Session.SetString(InitialAppStateKey, jsonState);
                    }
                }
                catch (InvalidOperationException)
                {
                    _logger.LogWarning("Session not available when trying to set InitialAppState");
                }

                _logger.LogDebug("Successfully set InitialAppState to storage");
            }
            else
            {
                _logger.LogWarning("HttpContext was null when setting InitialAppState");
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting InitialAppState to storage");
        }
    }

    public AppState? GetInitalAppStateFromSession()
    {
        try
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("HttpContext was null when getting InitialAppState");
                return null;
            }

            // Try to get from memory cache first
            string connectionId = httpContext.Connection.Id;
            if (_memoryCache.TryGetValue($"InitialAppState_{connectionId}", out AppState? cachedState) && cachedState != null)
            {
                return cachedState;
            }

            // Try to get from session if available
            try
            {
                if (httpContext.Session != null)
                {
                    string? jsonState = httpContext.Session.GetString(InitialAppStateKey);
                    if (jsonState != null)
                    {
                        AppState? result = JsonSerializer.Deserialize<AppState>(jsonState, _jsonOptions);

                        // Cache for future access
                        if (result != null)
                        {
                            _memoryCache.Set($"InitialAppState_{connectionId}", result,
                                new MemoryCacheEntryOptions
                                {
                                    SlidingExpiration = TimeSpan.FromMinutes(30)
                                });
                        }

                        return result;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                _logger.LogWarning("Session not available when trying to get InitialAppState");
                // Continue to return null
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving InitialAppState from storage");
            return null;
        }
    }
}