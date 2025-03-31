// PageBreadcrumbService.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Aviv.Base.UI.Services
{
    /// <summary>
    /// Represents a breadcrumb entry with text, URL, and active state
    /// </summary>
    public class BreadcrumbEntry
    {
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Service for managing breadcrumbs and page headings across the application
    /// </summary>
    public class PageBreadcrumbService : IDisposable
    {
        private readonly NavigationManager _navigationManager;
        private readonly List<BreadcrumbEntry> _breadcrumbs = [];
        private string _pageHeading = string.Empty;

        public IReadOnlyList<BreadcrumbEntry> Breadcrumbs => _breadcrumbs.AsReadOnly();
        public string PageHeading => _pageHeading;

        public event Action? OnChange;

        public PageBreadcrumbService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        }

        private async ValueTask OnLocationChanging(LocationChangingContext context)
        {
            // Clear breadcrumbs when navigation occurs
            Clear();
            // Navigation is occurring - we'll let the next page set its own breadcrumbs
            await ValueTask.CompletedTask;
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
            _pageHeading = heading;
            NotifyStateChanged();
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
        }

        /// <summary>
        /// Sets the page heading and replaces all breadcrumbs with the provided entries
        /// </summary>
        public void SetBreadcrumbs(string pageHeading, params (string Text, string Url, bool IsActive)[] crumbs)
        {
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
        }

        /// <summary>
        /// Sets the specified breadcrumb as active and all others as inactive
        /// </summary>
        public void SetActive(int index)
        {
            if (index >= 0 && index < _breadcrumbs.Count)
            {
                for (int i = 0; i < _breadcrumbs.Count; i++)
                {
                    _breadcrumbs[i].IsActive = (i == index);
                }

                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void Dispose()
        {
            // Unregister navigation event handler if needed
        }
    }
}