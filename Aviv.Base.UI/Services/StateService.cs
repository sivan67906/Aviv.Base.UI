using Aviv.Base.UI.Models;
using Aviv.Base.UI.Services;
using Microsoft.JSInterop;

// Make sure to clear the Session if stored. As the values are override by the session value automatically.
// If any Changes to values then the priority will move to the default values rather than the session.
public class AppState
{
    public string DataToggledId { get; set; } = "close";                   //
    public string ColorTheme { get; set; } = "light";                   // light, dark
    public string Direction { get; set; } = "ltr";                      // ltr, rtl
    public string NavigationStyles { get; set; } = "vertical";          // vertical, horizontal   
    public string MenuStyles { get; set; } = "";                        // menu-click, menu-hover, icon-click, icon-hover
    public string LayoutStyles { get; set; } = "default-menu";          // doublemenu, detached, icon-overlay, icontext-menu, closed-menu, default-menu 
    public string PageStyles { get; set; } = "regular";                 // regular, classic, modern
    public string WidthStyles { get; set; } = "fullwidth";              // fullwidth, boxed
    public string MenuPosition { get; set; } = "fixed";                 // fixed, scrollable
    public string HeaderPosition { get; set; } = "fixed";               // fixed, scrollable
    public string MenuColor { get; set; } = "dark";                     // light, dark, color, gradient, transparent
    public string HeaderColor { get; set; } = "light";                  // light, dark, color, gradient, transparent
    public string ThemePrimary { get; set; } = "";                      // '58, 88, 146', '92, 144, 163', '161, 90, 223', '78, 172, 76', '223, 90, 90'
    public string ThemeBackground { get; set; } = "";                   //make sure to add rgb valies like example :- '58, 88, 146' and also same for ThemeBackground1
    public string ThemeBackground1 { get; set; } = "";
    public string BackgroundImage { get; set; } = "";                   // bgimg1, bgimg2, bgimg3, bgimg4, bgimg5
    public MainMenuItems? currentItem { get; set; } = null;


    public bool IsDifferentFrom(AppState other)
    {
        if (other == null) return true;

        return DataToggledId != other.DataToggledId ||
               ColorTheme != other.ColorTheme ||
               Direction != other.Direction ||
               NavigationStyles != other.NavigationStyles ||
               MenuStyles != other.MenuStyles ||
               LayoutStyles != other.LayoutStyles ||
               PageStyles != other.PageStyles ||
               WidthStyles != other.WidthStyles ||
               MenuPosition != other.MenuPosition ||
               HeaderPosition != other.HeaderPosition ||
               MenuColor != other.MenuColor ||
               HeaderColor != other.HeaderColor ||
               ThemePrimary != other.ThemePrimary ||
               ThemeBackground != other.ThemeBackground ||
               ThemeBackground1 != other.ThemeBackground1 ||
               BackgroundImage != other.BackgroundImage ||
               (currentItem != null ? !currentItem.Equals(other.currentItem) : other.currentItem != null);
    }

    // Override Equals method to compare properties
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        AppState other = (AppState)obj;

        // Compare each property
        return DataToggledId == other.DataToggledId &&
               ColorTheme == other.ColorTheme &&
               Direction == other.Direction &&
               NavigationStyles == other.NavigationStyles &&
               MenuStyles == other.MenuStyles &&
               LayoutStyles == other.LayoutStyles &&
               PageStyles == other.PageStyles &&
               WidthStyles == other.WidthStyles &&
               MenuPosition == other.MenuPosition &&
               HeaderPosition == other.HeaderPosition &&
               MenuColor == other.MenuColor &&
               HeaderColor == other.HeaderColor &&
               ThemePrimary == other.ThemePrimary &&
               ThemeBackground == other.ThemeBackground &&
               ThemeBackground1 == other.ThemeBackground1 &&
               BackgroundImage == other.BackgroundImage &&
               Equals(currentItem, other.currentItem);
    }

    // Override GetHashCode if you override Equals
    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(DataToggledId);
        hash.Add(ColorTheme);
        hash.Add(Direction);
        hash.Add(NavigationStyles);
        hash.Add(MenuStyles);
        hash.Add(LayoutStyles);
        hash.Add(PageStyles);
        hash.Add(WidthStyles);
        hash.Add(MenuPosition);
        hash.Add(HeaderPosition);
        hash.Add(MenuColor);
        hash.Add(HeaderColor);
        hash.Add(ThemePrimary);
        hash.Add(ThemeBackground);
        hash.Add(ThemeBackground1);
        hash.Add(BackgroundImage);
        hash.Add(currentItem);
        return hash.ToHashCode();
    }

    public async Task InitializeFromSession(AppState sessionState, SessionService _sessionService)
    {
        AppState _currentState = new AppState();
        AppState? stored = _sessionService.GetInitalAppStateFromSession();
        if (stored != null && _currentState.IsDifferentFrom(stored))
        {
            DataToggledId = DataToggledId;
            ColorTheme = ColorTheme;
            Direction = Direction;
            NavigationStyles = NavigationStyles;
            MenuStyles = MenuStyles;
            LayoutStyles = LayoutStyles;
            PageStyles = PageStyles;
            WidthStyles = WidthStyles;
            MenuPosition = MenuPosition;
            HeaderPosition = HeaderPosition;
            MenuColor = MenuColor;
            HeaderColor = HeaderColor;
            ThemePrimary = ThemePrimary;
            ThemeBackground = ThemeBackground;
            ThemeBackground1 = ThemeBackground1;
            BackgroundImage = BackgroundImage;
            currentItem = currentItem;
            await _sessionService.SetInitalAppStateToSession(_currentState);
        }
        // Check and assign session values if present
        else if (sessionState != null)
        {
            DataToggledId = sessionState.DataToggledId;
            ColorTheme = sessionState.ColorTheme;
            Direction = sessionState.Direction;
            NavigationStyles = sessionState.NavigationStyles;
            MenuStyles = sessionState.MenuStyles;
            LayoutStyles = sessionState.LayoutStyles;
            PageStyles = sessionState.PageStyles;
            WidthStyles = sessionState.WidthStyles;
            MenuPosition = sessionState.MenuPosition;
            HeaderPosition = sessionState.HeaderPosition;
            MenuColor = sessionState.MenuColor;
            HeaderColor = sessionState.HeaderColor;
            ThemePrimary = sessionState.ThemePrimary;
            ThemeBackground = sessionState.ThemeBackground;
            ThemeBackground1 = sessionState.ThemeBackground1;
            BackgroundImage = sessionState.BackgroundImage;
            currentItem = sessionState.currentItem;
        }
    }
}


public class StateService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly SessionService _sessionService;
    private readonly AppState _currentState;
    private readonly ILogger<AppState> _logger;
    private readonly ThemePresetService _themePresetService;
    private readonly NotificationCustomService _notificationService;


    // Cache for JS interop calls to reduce redundant calls
    private readonly Dictionary<string, object> _jsCache = [];
    private readonly SemaphoreSlim _jsLock = new SemaphoreSlim(1, 1);
    private bool _isInitialized = false;

    public AppState GetAppState()
    {
        return _currentState;
    }

    public event Action OnChange;
    // Event to notify subscribers about state changes
    public event Action? OnStateChanged;

    public StateService(IJSRuntime jsRuntime, SessionService sessionService, AppState appState,
        ILogger<AppState> logger, ThemePresetService themePresetService,
        NotificationCustomService notificationService)
    {
        _jsRuntime = jsRuntime;
        _sessionService = sessionService;
        _currentState = new AppState();
        OnChange = () => { };
        _logger = logger;
        _themePresetService = themePresetService;
        _notificationService = notificationService;
        _themePresetService.OnPresetChanged += () => OnChange?.Invoke();

        Task.Run(async () => await InitializeAppStateAsync());
    }

    private async Task InitializeAppStateAsync()
    {
        try
        {
            // Retrieve session values asynchronously
            AppState sessionState = _sessionService.GetAppStateFromSession();
            AppState? initialAppState = _sessionService.GetInitalAppStateFromSession();
            if (initialAppState == null)
            {
                await _sessionService.SetInitalAppStateToSession(_currentState);
            }
            // Initialize AppState from session or default values
            await _currentState.InitializeFromSession(sessionState, _sessionService);

            // Notify state change if needed
            OnChange?.Invoke();
            NotifyStateChanged();

            _isInitialized = true;
            _logger.LogInformation("AppState initialized successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing AppState");
        }
    }

    public async Task WaitForInitialization()
    {
        // Wait for initialization to complete with timeout
        int attempts = 0;
        while (!_isInitialized && attempts < 20)
        {
            await Task.Delay(100);
            attempts++;
        }

        if (!_isInitialized)
        {
            _logger.LogWarning("StateService initialization took longer than expected");
        }
    }

    public async Task InitializeLandingAppState()
    {
        // Retrieve session values
        AppState sessionState = _sessionService.GetAppStateFromSession();
        _currentState.NavigationStyles = "horizontal";
        _currentState.MenuStyles = "menu-click";
        // Initialize AppState from session or default values
        await _currentState.InitializeFromSession(sessionState, _sessionService);

        // Notify state change
        NotifyStateChanged();
    }

    private async void NotifyStateChanged()
    {
        await _sessionService.SetAppStateToSession(_currentState);
        // Invoke the event to notify subscribers
        OnStateChanged?.Invoke();
    }

    // Helper method for JS interop caching
    private async Task<T> InvokeJSCached<T>(string method, params object[] args)
    {
        string cacheKey = $"{method}_{string.Join("_", args)}";

        await _jsLock.WaitAsync();
        try
        {
            if (_jsCache.TryGetValue(cacheKey, out object? cachedValue))
            {
                return (T)cachedValue;
            }

            T result = await _jsRuntime.InvokeAsync<T>(method, args);
            _jsCache[cacheKey] = result!;
            return result;
        }
        finally
        {
            _jsLock.Release();
        }
    }

    private async Task InvokeJSVoidCached(string method, params object[] args)
    {
        await _jsRuntime.InvokeVoidAsync(method, args);
    }

    // Clear JS cache to ensure fresh values
    private void ClearJSCache()
    {
        _jsLock.Wait();
        try
        {
            _jsCache.Clear();
        }
        finally
        {
            _jsLock.Release();
        }
    }

    public async Task directionFn(string val)
    {
        _currentState.Direction = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "dir", val);
        NotifyStateChanged();
    }

    public Task setCurrentItem(MainMenuItems val)
    {
        _currentState.currentItem = val;
        return Task.CompletedTask;
    }

    public async Task colorthemeFn(string val, bool stateClick)
    {
        _currentState.ColorTheme = val;

        if (stateClick)
        {
            _currentState.ThemeBackground = "";
            _currentState.ThemeBackground1 = "";
        }

        // Group related JS calls to reduce interop overhead
        await InvokeJSVoidCached("interop.setclearCssVariables");
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-theme-mode", val);
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-header-styles", val);
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-menu-styles", val);

        if (stateClick)
        {
            if (val == "light")
            {
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-menu-styles", "dark");
                await menuColorFn("dark");
            }
            else
            {
                await menuColorFn(val);
            }
            await headerColorFn(val);
        }

        await InvokeJSVoidCached("interop.removeCssVariable", "--body-bg-rgb");
        await InvokeJSVoidCached("interop.removeCssVariable", "--body-bg-rgb2");
        await InvokeJSVoidCached("interop.removeCssVariable", "--light-rgb");
        await InvokeJSVoidCached("interop.removeCssVariable", "--form-control-bg");
        await InvokeJSVoidCached("interop.removeCssVariable", "--input-border");

        NotifyStateChanged();
        await PersistState();
    }

    private int screenSize = 1268;

    public async Task navigationStylesFn(string val, bool stateClick)
    {
        try
        {
            if (string.IsNullOrEmpty(_currentState.MenuStyles) && val == "horizontal")
            {
                _currentState.MenuStyles = "menu-click";
                _currentState.LayoutStyles = "";
                await menuStylesFn("menu-click");
            }
            if (stateClick && val == "vertical")
            {
                _currentState.MenuStyles = "";
                _currentState.LayoutStyles = "default-menu";
            }

            _currentState.NavigationStyles = val;
            await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", val);

            if (val == "horizontal")
            {
                await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-vertical-style");
            }
            else
            {
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", val);
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "overlay");
                await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-nav-style");

                if (await _jsRuntime.InvokeAsync<int>("interop.inner", "innerWidth") > 992)
                {
                    await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-toggled");
                }
            }

            screenSize = await _jsRuntime.InvokeAsync<int>("interop.inner", "innerWidth");

            if (screenSize < 992)
            {
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "close");
            }

            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in navigationStylesFn");
        }
    }

    public async Task layoutStylesFn(string val)
    {
        try
        {
            _currentState.LayoutStyles = val;
            _currentState.MenuStyles = "";

            await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-nav-style");

            switch (val)
            {
                case "default-menu":
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "overlay");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    if (await _jsRuntime.InvokeAsync<int>("interop.inner", "innerWidth") > 992)
                    {
                        await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-toggled");
                    }
                    break;
                case "closed-menu":
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "closed");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "close-menu-close");
                    break;
                case "detached":
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "detached");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "detached-close");
                    break;
                case "icontext-menu":
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "icontext");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "icon-text-close");
                    break;
                case "icon-overlay":
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "overlay");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "icon-overlay-close");
                    break;
                case "double-menu":
                    bool isdoubleMenuActive = await _jsRuntime.InvokeAsync<bool>("interop.isEleExist", ".double-menu-active");

                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-vertical-style", "doublemenu");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-layout", "vertical");
                    await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "double-menu-open");
                    if (!isdoubleMenuActive)
                    {
                        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "double-menu-close");
                    }
                    break;
            }

            screenSize = await _jsRuntime.InvokeAsync<int>("interop.inner", "innerWidth");

            if (screenSize < 992)
            {
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "close");
            }

            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in layoutStylesFn");
        }
    }

    public async Task menuStylesFn(string val)
    {
        try
        {
            _currentState.LayoutStyles = "";
            _currentState.MenuStyles = val;

            await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-vertical-style");
            await InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-hor-style");
            await InvokeJSVoidCached("interop.addAttributeToHtml", "data-nav-style", val);
            await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", $"{val}-closed");

            screenSize = await _jsRuntime.InvokeAsync<int>("interop.inner", "innerWidth");

            if (screenSize < 992)
            {
                await InvokeJSVoidCached("interop.addAttributeToHtml", "data-toggled", "close");
            }
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in menuStylesFn");
        }
    }

    public async Task pageStyleFn(string val)
    {
        _currentState.PageStyles = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-page-style", val);
        NotifyStateChanged();
    }

    public async Task widthStylessFn(string val)
    {
        _currentState.WidthStyles = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-width", val);
        NotifyStateChanged();
    }

    public async Task menuPositionFn(string val)
    {
        _currentState.MenuPosition = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-menu-position", val);
        NotifyStateChanged();
    }

    public async Task headerPositionFn(string val)
    {
        _currentState.HeaderPosition = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-header-position", val);
        NotifyStateChanged();
    }

    public async Task menuColorFn(string val)
    {
        _currentState.MenuColor = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-menu-styles", val);
        NotifyStateChanged();
    }

    public async Task headerColorFn(string val)
    {
        _currentState.HeaderColor = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-header-styles", val);
        NotifyStateChanged();
    }

    public async Task themePrimaryFn(string val)
    {
        _currentState.ThemePrimary = val;
        await InvokeJSVoidCached("interop.setCssVariable", "--primary-rgb", val);
        NotifyStateChanged();
    }

    public async Task themeBackgroundFn(string val, string val2, bool stateClick)
    {
        try
        {
            _currentState.ThemeBackground = val;
            _currentState.ThemeBackground1 = val2;

            // Group related JS calls to reduce interop overhead
            List<Task> tasks =
            [
                InvokeJSVoidCached("interop.addAttributeToHtml", "data-theme-mode", "dark"),
                InvokeJSVoidCached("interop.addAttributeToHtml", "data-header-styles", "dark"),
                InvokeJSVoidCached("interop.addAttributeToHtml", "data-menu-styles", "dark")
            ];

            await Task.WhenAll(tasks);

            _currentState.ColorTheme = "dark";
            if (stateClick)
            {
                _currentState.MenuColor = "dark";
                _currentState.HeaderColor = "dark";
            }

            // Set CSS variables
            tasks =
            [
                InvokeJSVoidCached("interop.setCssVariable", "--body-bg-rgb", val),
                InvokeJSVoidCached("interop.setCssVariable", "--body-bg-rgb2", val2),
                InvokeJSVoidCached("interop.setCssVariable", "--light-rgb", val2),
                InvokeJSVoidCached("interop.setCssVariable", "--form-control-bg", $"rgb({val2})"),
                InvokeJSVoidCached("interop.setCssVariable", "--input-border", "rgba(255,255,255,0.1)")
            ];

            await Task.WhenAll(tasks);

            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in themeBackgroundFn");
        }
    }

    public async Task backgroundImageFn(string val)
    {
        _currentState.BackgroundImage = val;
        await InvokeJSVoidCached("interop.addAttributeToHtml", "data-bg-img", val);
        NotifyStateChanged();
    }

    public async Task reset()
    {
        // Reset all state values to defaults
        _currentState.ColorTheme = "light";
        _currentState.Direction = "ltr";
        _currentState.NavigationStyles = "vertical";
        _currentState.MenuStyles = "";
        _currentState.LayoutStyles = "default-menu";
        _currentState.PageStyles = "regular";
        _currentState.WidthStyles = "fullwidth";
        _currentState.MenuPosition = "fixed";
        _currentState.HeaderPosition = "fixed";
        _currentState.MenuColor = "dark";
        _currentState.HeaderColor = "light";
        _currentState.ThemePrimary = "";
        _currentState.ThemeBackground = "";
        _currentState.ThemeBackground1 = "";
        _currentState.BackgroundImage = "";

        // Clear JS cache to ensure fresh values
        ClearJSCache();

        // Clearing localstorage
        await InvokeJSVoidCached("interop.clearAllLocalStorage");
        await InvokeJSVoidCached("interop.setclearCssVariables");

        // Reseting to light
        await colorthemeFn("light", false);

        // To reset the light-rgb
        await InvokeJSVoidCached("interop.removeAttributeFromHtml", "style");

        // Clear attributes in batches to reduce interop overhead
        List<Task> tasks =
        [
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-nav-style"),
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-menu-position"),
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-header-position"),
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-page-style"),
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-width"),
            InvokeJSVoidCached("interop.removeAttributeFromHtml", "data-bg-img")
        ];

        await Task.WhenAll(tasks);

        // Reseting to ltr
        await directionFn("ltr");

        // Reseting to vertical
        await navigationStylesFn("vertical", false);

        // Resetting the menu Color
        await menuColorFn("dark");

        // Resetting the header Color
        await headerColorFn("light");

        _sessionService.DeleteAppStateFromSession();
        NotifyStateChanged();
    }

    public async Task Landingreset()
    {
        // Clearing localstorage
        await InvokeJSVoidCached("interop.clearAllLocalStorage");

        // Reseting to light
        await colorthemeFn("light", false);

        // To reset the light-rgb
        await InvokeJSVoidCached("interop.removeAttributeFromHtml", "style");

        // Reseting to ltr
        await directionFn("ltr");
        await menuColorFn("light");
        _currentState.ThemePrimary = "";
        _sessionService.DeleteAppStateFromSession();
        NotifyStateChanged();
    }

    public async Task retrieveFromLocalStorage()
    {
        try
        {
            // Wait for initialization to complete
            await WaitForInitialization();

            // Direction
            string direction = _currentState.Direction;
            await directionFn(direction);

            // Navigation styles
            string navstyles = _currentState.NavigationStyles;
            await navigationStylesFn(navstyles, false);

            // Page style
            string pageStyle = _currentState.PageStyles;
            await pageStyleFn(pageStyle);

            // Width styles
            string widthStyles = _currentState.WidthStyles;
            await widthStylessFn(widthStyles);

            // Menu position
            string ynexmenuposition = _currentState.MenuPosition;
            await menuPositionFn(ynexmenuposition);

            // Header position
            string ynexheaderposition = _currentState.HeaderPosition;
            await headerPositionFn(ynexheaderposition);

            // Color theme
            string ynexcolortheme = _currentState.ColorTheme;
            await colorthemeFn(ynexcolortheme, false);

            // Background image
            string ynexbgimg = _currentState.BackgroundImage;
            if (!string.IsNullOrEmpty(ynexbgimg))
            {
                await backgroundImageFn(ynexbgimg);
            }

            // Background color
            string ynexbgcolor = _currentState.ThemeBackground;
            string ynexbgcolor1 = _currentState.ThemeBackground1;
            if (!string.IsNullOrEmpty(ynexbgcolor))
            {
                await themeBackgroundFn(ynexbgcolor, ynexbgcolor1, false);
                _currentState.ColorTheme = "dark";
            }

            // Menu color
            string ynexMenu = _currentState.MenuColor;
            await menuColorFn(ynexMenu);

            // Header color
            string ynexHeader = _currentState.HeaderColor;
            await headerColorFn(ynexHeader);

            // Menu and layout styles
            string ynexmenuStyles = _currentState.MenuStyles;
            string ynexverticalstyles = _currentState.LayoutStyles;
            if (string.IsNullOrEmpty(ynexverticalstyles))
            {
                await menuStylesFn(ynexmenuStyles);
            }
            else
            {
                await layoutStylesFn(ynexverticalstyles);
            }

            // Primary theme color
            string ynexprimaryRGB = _currentState.ThemePrimary;
            await themePrimaryFn(ynexprimaryRGB);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in retrieveFromLocalStorage");
        }
    }

    public async Task retrieveFromLandingLocalStorage()
    {
        try
        {
            // Wait for initialization to complete
            await WaitForInitialization();

            // Reseting to horizontal layout
            await navigationStylesFn("horizontal", false);
            _currentState.MenuStyles = "menu-click";
            _currentState.LayoutStyles = "";
            await menuStylesFn("menu-click");

            // Direction
            string direction = await _jsRuntime.InvokeAsync<string>("interop.getLocalStorageItem", "ynexdirection") ?? _currentState.Direction;
            await directionFn(direction);

            // Color theme
            string ynexcolortheme = await _jsRuntime.InvokeAsync<string>("interop.getLocalStorageItem", "ynexcolortheme") ?? _currentState.ColorTheme;
            await colorthemeFn(ynexcolortheme, false);

            // Primary color
            string ynexprimaryRGB = await _jsRuntime.InvokeAsync<string>("interop.getLocalStorageItem", "ynexprimaryRGB") ?? _currentState.ThemePrimary;
            await themePrimaryFn(ynexprimaryRGB);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in retrieveFromLandingLocalStorage");
        }
    }

    private async Task PersistState()
    {
        try
        {
            // Persist state to session
            await _sessionService.SetAppStateToSession(_currentState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error persisting state");
        }
    }

    // Theme Preset Switching Method
    // Add this method to the StateService class
    public async Task SwitchThemePreset(string presetId)
    {
        ThemePreset? preset = _themePresetService.GetPreset(presetId);
        if (preset == null)
        {
            _logger.LogWarning("Theme preset {PresetId} not found", presetId);
            await _notificationService.ShowErrorAsync($"Theme preset '{presetId}' not found", true);
            return;
        }

        // Apply the theme settings from the preset
        AppState settings = preset.ThemeSettings;

        // Apply each setting (order matters for some settings)
        await directionFn(settings.Direction);
        await colorthemeFn(settings.ColorTheme, false);
        await navigationStylesFn(settings.NavigationStyles, false);

        if (!string.IsNullOrEmpty(settings.LayoutStyles))
        {
            await layoutStylesFn(settings.LayoutStyles);
        }
        else if (!string.IsNullOrEmpty(settings.MenuStyles))
        {
            await menuStylesFn(settings.MenuStyles);
        }

        await pageStyleFn(settings.PageStyles);
        await widthStylessFn(settings.WidthStyles);
        await menuPositionFn(settings.MenuPosition);
        await headerPositionFn(settings.HeaderPosition);
        await menuColorFn(settings.MenuColor);
        await headerColorFn(settings.HeaderColor);

        if (!string.IsNullOrEmpty(settings.ThemePrimary))
        {
            await themePrimaryFn(settings.ThemePrimary);
        }

        if (!string.IsNullOrEmpty(settings.ThemeBackground) && !string.IsNullOrEmpty(settings.ThemeBackground1))
        {
            await themeBackgroundFn(settings.ThemeBackground, settings.ThemeBackground1, false);
        }

        if (!string.IsNullOrEmpty(settings.BackgroundImage))
        {
            await backgroundImageFn(settings.BackgroundImage);
        }

        // Set the active preset
        _themePresetService.SetActivePreset(presetId);

        // Apply theme preset HTML attributes
        await _themePresetService.ApplyThemePresetAttributes(_jsRuntime);

        // Update state
        NotifyStateChanged();

        // Show success notification with appropriate message based on theme
        string message = GetThemeChangeMessage(presetId);
        await _notificationService.ShowSuccessAsync(message, true);
    }

    // Helper method to get appropriate message for each theme
    private string GetThemeChangeMessage(string presetId)
    {
        return presetId switch
        {
            "default" => "Switched to Main Theme",
            "product_manager" => "Switched to Product Manager Theme",
            "support" => "Switched to Support Theme",
            _ => $"Theme changed to {presetId}"
        };
    }

}