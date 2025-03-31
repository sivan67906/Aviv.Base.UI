using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

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
                // Store in session
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
                // Remove from session
                httpContext.Session.Remove(AppStateKey);

                // Remove from memory cache
                _memoryCache.Remove($"AppState_{httpContext.Connection.Id}");

                _logger.LogDebug("Successfully deleted AppState from session");
            }
            else
            {
                _logger.LogWarning("HttpContext was null when deleting AppState");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting AppState from session");
        }
    }

    public AppState GetAppStateFromSession()
    {
        try
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            // Check if HttpContext or Session is null
            if (httpContext == null || httpContext.Session == null)
            {
                _logger.LogWarning("HttpContext or Session was null when getting AppState");
                return new AppState(); // Return default state
            }

            // Try to get from memory cache first for better performance
            string connectionId = httpContext.Connection.Id;
            if (_memoryCache.TryGetValue($"AppState_{connectionId}", out AppState? cachedState) && cachedState != null)
            {
                return cachedState;
            }

            // If not in cache, try to get from session
            string? jsonState = httpContext.Session.GetString(AppStateKey);
            if (string.IsNullOrEmpty(jsonState))
            {
                _logger.LogDebug("No AppState found in session, returning default");
                return new AppState(); // Return default state if session data is not available
            }

            // Deserialize the state
            AppState? appState = JsonSerializer.Deserialize<AppState>(jsonState, _jsonOptions);

            // Check if deserialization succeeded and appState is not null
            if (appState == null)
            {
                _logger.LogWarning("Deserialization of AppState from session returned null");
                return new AppState(); // Return default state if deserialization failed
            }

            // Cache the result for faster access next time
            _memoryCache.Set($"AppState_{connectionId}", appState,
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30),
                    Priority = CacheItemPriority.High
                });

            return appState;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AppState from session");
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
                string jsonState = JsonSerializer.Serialize(state, _jsonOptions);
                httpContext.Session.SetString(InitialAppStateKey, jsonState);

                // Also cache in memory
                _memoryCache.Set($"InitialAppState_{httpContext.Connection.Id}", state,
                    new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(30)
                    });

                _logger.LogDebug("Successfully set InitialAppState to session");
            }
            else
            {
                _logger.LogWarning("HttpContext was null when setting InitialAppState");
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting InitialAppState to session");
        }
    }

    public  AppState? GetInitalAppStateFromSession()
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

            ISession? session = httpContext.Session;
            if (session == null)
            {
                _logger.LogWarning("Session was null when getting InitialAppState");
                return null;
            }

            string? jsonState = session.GetString(InitialAppStateKey);
            if (jsonState == null)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AppState>(jsonState, _jsonOptions);

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving InitialAppState from session");
            return null;
        }
    }
}