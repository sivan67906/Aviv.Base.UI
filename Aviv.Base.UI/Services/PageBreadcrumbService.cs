// PageBreadcrumbService.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Aviv.Base.UI.Services;

/// <summary>
/// Represents a breadcrumb entry with text, URL, and active state
/// </summary>
public class BreadcrumbEntry
{
    public string Text { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // For efficient equality comparison
    public override bool Equals(object? obj)
    {
        if (obj is not BreadcrumbEntry other)
            return false;

        return Text == other.Text &&
               Url == other.Url &&
               IsActive == other.IsActive;
    }

    public override int GetHashCode() => HashCode.Combine(Text, Url, IsActive);

    // For deep copying
    public BreadcrumbEntry Clone() => new BreadcrumbEntry
    {
        Text = Text,
        Url = Url,
        IsActive = IsActive
    };
}

/// <summary>
/// Service for managing breadcrumbs and page headings across the application
/// </summary>
public class PageBreadcrumbService : IDisposable
{
    private readonly NavigationManager _navigationManager;
    private readonly ILogger<PageBreadcrumbService> _logger;
    private readonly IMemoryCache _cache;

    private readonly List<BreadcrumbEntry> _breadcrumbs = [];
    private string _pageHeading = string.Empty;
    private bool _suppressNotifications = false;
    private CancellationTokenSource? _debounceTokenSource;

    // Cache breadcrumb configurations for common paths
    private static readonly ConcurrentDictionary<string, (string Heading, List<BreadcrumbEntry> Breadcrumbs)>
        _breadcrumbCache = new();

    public IReadOnlyList<BreadcrumbEntry> Breadcrumbs => _breadcrumbs.AsReadOnly();
    public string PageHeading => _pageHeading;

    public event Action? OnChange;

    public PageBreadcrumbService(
        NavigationManager navigationManager,
        ILogger<PageBreadcrumbService> logger,
        IMemoryCache cache)
    {
        _navigationManager = navigationManager;
        _logger = logger;
        _cache = cache;

        _navigationManager.RegisterLocationChangingHandler(OnLocationChanging);
    }

    private async ValueTask OnLocationChanging(LocationChangingContext context)
    {
        try
        {
            // Clear breadcrumbs when navigation occurs
            Clear();

            // Pre-load cached breadcrumbs for the new location if available
            string path = new Uri(context.TargetLocation).AbsolutePath;
            if (_breadcrumbCache.TryGetValue(path, out var cached))
            {
                _suppressNotifications = true;

                _pageHeading = cached.Heading;
                _breadcrumbs.Clear();
                foreach (var crumb in cached.Breadcrumbs)
                {
                    _breadcrumbs.Add(crumb.Clone());
                }

                _suppressNotifications = false;
                NotifyStateChanged();
            }

            // Navigation is occurring - we'll let the next page set its own breadcrumbs
            await ValueTask.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in OnLocationChanging");
        }
    }

    /// <summary>
    /// Clears all breadcrumbs and page heading
    /// </summary>
    public void Clear()
    {
        _breadcrumbs.Clear();
        _pageHeading = string.Empty;
        NotifyStateChanged();
    }

    /// <summary>
    /// Sets the page heading without modifying breadcrumbs
    /// </summary>
    public void SetPageHeading(string heading)
    {
        if (_pageHeading == heading)
            return;

        _pageHeading = heading;
        NotifyStateChanged();

        // Update cache for current path
        UpdateCache();
    }

    /// <summary>
    /// Adds a single breadcrumb entry
    /// </summary>
    public void Add(string text, string url, bool isActive = false)
    {
        _breadcrumbs.Add(new BreadcrumbEntry
        {
            Text = text,
            Url = url,
            IsActive = isActive
        });

        NotifyStateChanged();
        UpdateCache();
    }

    /// <summary>
    /// Sets the page heading and replaces all breadcrumbs with the provided entries
    /// </summary>
    public void SetBreadcrumbs(string pageHeading, params (string Text, string Url, bool IsActive)[] crumbs)
    {
        bool hasChanged = _pageHeading != pageHeading || crumbs.Length != _breadcrumbs.Count;

        _pageHeading = pageHeading;
        _breadcrumbs.Clear();

        foreach ((string text, string url, bool isActive) in crumbs)
        {
            _breadcrumbs.Add(new BreadcrumbEntry
            {
                Text = text,
                Url = url,
                IsActive = isActive
            });
        }

        NotifyStateChanged();
        UpdateCache();
    }

    /// <summary>
    /// Sets the page heading and creates a path of breadcrumbs with the last item active
    /// </summary>
    public void SetBreadcrumbPath(string pageHeading, params (string Text, string Url)[] path)
    {
        _pageHeading = pageHeading;
        _breadcrumbs.Clear();

        for (int i = 0; i < path.Length; i++)
        {
            (string text, string url) = path[i];
            _breadcrumbs.Add(new BreadcrumbEntry
            {
                Text = text,
                Url = url,
                IsActive = i == path.Length - 1
            });
        }

        NotifyStateChanged();
        UpdateCache();
    }

    /// <summary>
    /// Sets the specified breadcrumb as active and all others as inactive
    /// </summary>
    public void SetActive(int index)
    {
        if (index >= 0 && index < _breadcrumbs.Count)
        {
            bool changed = false;

            for (int i = 0; i < _breadcrumbs.Count; i++)
            {
                bool shouldBeActive = (i == index);
                if (_breadcrumbs[i].IsActive != shouldBeActive)
                {
                    _breadcrumbs[i].IsActive = shouldBeActive;
                    changed = true;
                }
            }

            if (changed)
            {
                NotifyStateChanged();
                UpdateCache();
            }
        }
    }

    // Update cache with current breadcrumb state
    private void UpdateCache()
    {
        try
        {
            var currentPath = new Uri(_navigationManager.Uri).AbsolutePath;

            // Create a deep copy of breadcrumbs for the cache
            var breadcrumbsCopy = _breadcrumbs.Select(b => b.Clone()).ToList();

            _breadcrumbCache[currentPath] = (_pageHeading, breadcrumbsCopy);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating breadcrumb cache");
        }
    }

    // Notify state changed with debouncing to prevent excessive UI updates
    private void NotifyStateChanged()
    {
        if (_suppressNotifications)
            return;

        try
        {
            // Cancel any pending notification
            _debounceTokenSource?.Cancel();
            _debounceTokenSource?.Dispose();
            _debounceTokenSource = new CancellationTokenSource();
            var token = _debounceTokenSource.Token;

            // Trigger notification after a short delay (debouncing)
            Task.Delay(10, token).ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully && !token.IsCancellationRequested)
                {
                    OnChange?.Invoke();
                }
            }, TaskScheduler.Default);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotifyStateChanged");
            OnChange?.Invoke();
        }
    }

    public void Dispose()
    {
        _debounceTokenSource?.Cancel();
        _debounceTokenSource?.Dispose();
        _debounceTokenSource = null;
    }
}